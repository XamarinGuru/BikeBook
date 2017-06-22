'use strict';

const user = require('../models/user');
const bcrypt = require('bcryptjs');

exports.registerUser = (name, email, picture, styleSport, styleTouring,
    styleCruising, styleAdventure, styleTrack, styleCommuting, password) => 

	new Promise((resolve,reject) => {

	    const salt = bcrypt.genSaltSync(10);
        const hash = bcrypt.hashSync(password, salt);

		const newUser = new user({

			name: name,
            email: email,
            picture: picture,
            styleSport: styleSport,
            styleTouring: styleTouring,
            styleCruising: styleCruising,
            styleAdventure: styleAdventure,
            styleTrack: styleTrack,
            styleCommuting: styleCommuting,
			hashed_password: hash,
			created_at: new Date()
		});

		newUser.save()

		.then(() => resolve({ status: 201, message: 'User Registered Sucessfully !' }))

		.catch(err => {

			if (err.code == 11000) {
						
				reject({ status: 409, message: 'User Already Registered !' });

			} else {

				reject({ status: 500, message: 'Internal Server Error !' });
			}
		});
	});


