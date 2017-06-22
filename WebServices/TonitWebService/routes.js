'use strict';

const auth = require('basic-auth');
const jwt = require('jsonwebtoken');

const register = require('./functions/register');
const login = require('./functions/login');
const profile = require('./functions/profile');
const password = require('./functions/password');
const config = require('./config/config.json');
const messageing = require('./functions/messaging')
const posting = require('./functions/posting')
const addManager = require('./functions/classifiedAdManager')

module.exports = router => {

	router.get('/', (req, res) => res.end('Welcome to Ton'));

    router.post('/login', (req, res) => {

        const email = req.body.email;
        const password = req.body.password;

        if (!email || !password || !email.trim() || !password.trim()) {
            res.status(400).json({ message: 'Invalid Request !' });
        } else {
            login.loginUser(email, password)
                .then(result => {
                    const token = jwt.sign(result, config.secret, { expiresIn: 1440 });
                    res.status(result.status).json({ message: result.message, token: token });
                })
                .catch(err => res.status(err.status).json({ message: err.message }));
        }
    });


	router.post('/users/register', (req, res) => {

		const name = req.body.name;
        const email = req.body.email;
        const picture = req.body.picture;
        const styleSport = req.body.styleSport;    
        const styleTouring = req.body.styleTouring; 
        const styleCruising = req.body.styleCruising; 
        const styleAdventure = req.body.styleAdventure; 
        const styleTrack = req.body.styleTrack; 
        const styleCommuting = req.body.styleCommuting;
		const password = req.body.password;

        if (!name || !email || !picture || !password || !name.trim() || !email.trim() || !password.trim()) {
			res.status(400).json({message: 'Invalid Request !'});
        } else {
            register.registerUser(name, email, picture, styleSport, styleTouring, styleCruising,
                styleAdventure, styleTrack, styleCommuting, password)
			.then(result => {
				res.setHeader('Location', '/users/'+email);
				res.status(result.status).json({ message: result.message })
			})
			.catch(err => res.status(err.status).json({ message: err.message }));
		}
	});


	router.get('/users/profile', (req,res) => {

		if (checkToken(req)) {
			profile.getProfile(req.params.id)
			.then(result => res.json(result))
			.catch(err => res.status(err.status).json({ message: err.message }));
		} else {
			res.status(401).json({ message: 'Invalid Token !' });
		}
	});


	router.put('/users/changePassword', (req,res) => {

		if (checkToken(req)) {

			const oldPassword = req.body.password;
            const newPassword = req.body.newPassword;
            const email = req.body.email;

            if (!oldPassword || !newPassword || !email || !oldPassword.trim() || !newPassword.trim() || !email.trim()) {

				res.status(400).json({ message: 'Invalid Request !' });

			} else {

                password.changePassword(email, oldPassword, newPassword)

				.then(result => res.status(result.status).json({ message: result.message }))

				.catch(err => res.status(err.status).json({ message: err.message }));

			}
		} else {

			res.status(401).json({ message: 'Invalid Token !' });
		}
    });


    router.get('/users/checkToken', (req, res) => {

        if (checkToken(req)) {
            res.status(200).json({ message: 'token validated' });
        } else {
            res.status(401).json({ message: 'token invalid' });
        }
    });

    router.put('/users/profileDescription', (req, res) => {

        const newProfileDescription = req.body.profileDescription;
        const email = req.headers['email'];
        if (checkToken(req)) {
            if (profile.changeProfileDescription(email, req.body.profileDescription)) {
                res.status(200).json({ message: 'profile desc updated' });
            }

        } else {
            res.status(401).json({ message: 'token invalid' });
        }
    });

    router.get('/users/profileDescription', (req, res) => {

        const email = req.headers['email'];
        if (checkToken(req))
        {
            profile.retrieveProfileDescription(email)
                .then(result => {
                    res.status(200).json({ description: result });
                })
                .catch(err => res.status(err.status).json({ message: err.message }));
        } else {
            res.status(401).json({ message: 'token invalid' });
        }
    });

    router.post('/users/updateProfile', (req, res) => {

        const name = req.body.name;
        const description = req.body.description;
        const styleSport = req.body.styleSport;
        const styleTouring = req.body.styleTouring;
        const styleCruising = req.body.styleCruising;
        const styleAdventure = req.body.styleAdventure;
        const styleTrack = req.body.styleTrack;
        const styleCommuting = req.body.styleCommuting;
        const picture = req.body.picture;

        const email = req.headers['email'];
        if (checkToken(req)) {
            if (profile.changeProfile(email, name, description, styleSport, styleTouring, styleCruising, styleAdventure, styleTrack, styleCommuting, picture)) {
                res.status(200).json({ message: 'profile desc updated' });
            }

        } else {
            res.status(401).json({ message: 'token invalid' });
        }
    });


    router.get('/users/getAllUsers', (req, res) => {

        const email = req.headers['email'];
        if (checkToken(req)) {
            profile.getAllUsers(email)
                .then(result => {
                    res.status(200).json({ user: result.users })
                })
                .catch(err => res.status(err.status).json({ message: err.message }));
        } else {
            res.status(401).json({ message: 'token invalid' });
        }
    });

    router.get('/users/getUser', (req, res) => {
        const email = req.headers['email'];
        const targetuseremail = req.headers['targetuseremail'];
        console.log("target " + targetuseremail);
        if (checkToken(req) || !targetuseremail || !targetuseremail.trim()) {
            profile.getUser(targetuseremail)
                .then(result => {
                    res.status(200).json({ user: result.users })
                })
                .catch(err => res.status(err.status).json({ message: err.message }));
        } else {
            res.status(401).json({ message: 'token invalid' });
        }

    });


	router.post('/users/resetPassword', (req,res) => {

        const email = req.body.email;
		const token = req.body.token;
		const newPassword = req.body.password;

		if (!token || !newPassword || !token.trim() || !newPassword.trim()) {

			password.resetPasswordInit(email)

			.then(result => res.status(result.status).json({ message: result.message }))

			.catch(err => res.status(err.status).json({ message: err.message }));

		} else {

			password.resetPasswordFinish(email, token, newPassword)

			.then(result => res.status(result.status).json({ message: result.message }))

			.catch(err => res.status(err.status).json({ message: err.message }));
		}
    });

    router.post('/message/send', (req, res) => {

        const email = req.headers['email'];
        const reciever = req.body.reciever;
        const message = req.body.message;
        const image = req.body.image;

        if (checkToken(req)) {
            messageing.sendMessage(email, reciever, message, image)
                .then(result => {
                    res.status(200).json({ message: 'sent item' });
                })
                .catch(err => res.status(err.status).json({ message: err.message }));
        } else {
            res.status(401).json({ message: 'token invalid' });
        }
    });

    router.get('/message/getMessages', (req, res) => {
        const email = req.headers['email'];

        if (checkToken(req))
        {
            messageing.GetMessages(email)
                .then(result => {
                    res.status(200).json({ message: result.messages });
                })
                .catch(err => res.status(err.status).json({ message: err.message }));
        } else {
            res.status(401).json({ message: 'token invalid' });
        }
    });

    //
    router.post('/ad/create', (req, res) => {

        const email = req.headers['email'];
        const title = req.body.title;
        const image = req.body.image;
        const description = req.body.description;
        const city = req.body.city;
        const provence = req.body.provence;

        if (checkToken(req)) {
            addManager.CreadAd(email, title, image, description, city, provence)
                .then(result => {
                    res.status(200).json({ message: 'created Add' });
                })
                .catch(err => res.status(err.status).json({ message: err.message }));
        } else {
            res.status(401).json({ message: 'token invalid' });
        }
    });

    router.get('/ad/get', (req, res) => {
        const email = req.headers['email'];

        if (checkToken(req)) {
            addManager.GetAds(email)
                .then(result => {
                    res.status(200).json({ ad: result.ads });
                })
                .catch(err => res.status(err.status).json({ message: err.message }));
        } else {
            res.status(401).json({ message: 'token invalid' });
        }
    });

    router.post('/post/create', (req, res) => {

        const email = req.headers['email'];
        const message = req.body.content;
        const image = req.body.image;

        if (checkToken(req)) {
            posting.createPost(email, image, message )
                .then(result => {
                    res.status(200).json({ message: 'post created' });
                })
                .catch(err => res.status(err.status).json({ message: err.message }));
        } else {
            res.status(401).json({ message: 'token invalid' });
        }
    });

    router.get('/post/get', (req, res) => {
        const email = req.headers['email'];

        if (checkToken(req)) {
            posting.GetPosts(email)
                .then(result => {
                    res.status(200).json({ post: result.posts });
                })
                .catch(err => res.status(err.status).json({ message: err.message }));
        } else {
            res.status(401).json({ message: 'token invalid' });
        }
    });

	function checkToken(req) {

        const token = req.headers['x-access-token'];
		if (token) {

			try {

                var decoded = jwt.verify(token, config.secret);
                return decoded.message === req.headers['email'];

            } catch (err) {
                console.log(err.message)
				return false;
			}

		} else {

			return false;
		}
    }
}