'use strict';

const user = require('../models/user');

exports.getProfile = email => 
	
	new Promise((resolve,reject) => {

		user.find({ email: email }, { name: 1, email: 1, created_at: 1, _id: 0 })

		.then(users => resolve(users[0]))

		.catch(err => reject({ status: 500, message: 'Internal Server Error !' }))

    });

exports.changeProfileDescription = (email, newDescription) =>
    new Promise((resolve, reject) => {

        user.find({ email: email })

            .then(users => {

                let user = users[0];
                user.description = newDescription;
                return user.save();
            })

            .then(user => resolve({ status: 200, message: 'profile desc changed sucessfully' }))

            .catch(err => reject({ status: 500, message: 'Internal Server Error !' }));
    });

exports.changeProfile = (email, name, description, styleSport, styleTouring, styleCruising,
                        styleAdventure, styleTrack, styleCommuting, picture) =>
    new Promise((resolve, reject) => {

        user.find({ email: email })

            .then(users => {
                let user = users[0];

                if (name != null && name != "") {
                    user.name = description;
                }
                if (description != null && description != "") {
                    user.description = description;
                }
                if (styleSport != null && styleSport != "") {
                    user.styleSport = styleSport;
                }
                if (styleTouring != null && styleTouring != "") {
                    user.styleTouring = styleTouring;
                }
                if (styleCruising != null && styleCruising != "") {
                    user.styleCruising = styleCruising;
                }
                if (styleAdventure != null && styleAdventure != "") {
                    user.styleAdventure = styleAdventure;
                }
                if (styleTrack != null && styleTrack != "") {
                    user.styleTrack = styleTrack;
                }
                if (styleCommuting != null && styleSport != "") {
                    user.styleCommuting = styleCommuting;
                }
                if (picture != null && picture != "") {
                    user.picture = picture;
                }

                return user.save();
            })

            .then(user => resolve({ status: 200, message: 'profile desc changed sucessfully' }))

            .catch(err => reject({ status: 500, message: 'Internal Server Error !' }));
    });


exports.retrieveProfileDescription = email =>

    new Promise((resolve, reject) => {

        user.find({ email: email }, { description: 1 })

            .then(users => resolve(users[0].description))
            .catch(err => reject({ status: 500, message: 'Internal Server Error !' }));

    });

exports.getAllUsers = email =>
    new Promise((resolve, reject) => {

        user.find({})
            .then(users => {
                resolve({users})
            })
            .catch(err => reject({ status: 500, message: 'Internal Server Error !' }));

    });
    
exports.getUser = email =>
    new Promise((resolve, reject) => {
        user.find({ email: email })
            .then(users => {
                resolve({users})
            })
            .catch(err => reject({ status: 500, message: 'Internal Server Error !' }));
        
    })