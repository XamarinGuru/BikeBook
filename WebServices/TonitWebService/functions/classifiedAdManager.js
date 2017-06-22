'use strict';

const ad = require('../models/classifiedAd');

exports.CreadAd = (Creator, Title, Image, Description, City, Provence) =>

    new Promise((resolve, reject) => {

        const newad = new ad({
            owner: Creator,
            title: Title,
            picture: Image,
            description: Description,
            city: City,
            provence: Provence,
            created_at: new Date()
        })

        newad.save()

            .then(() => resolve({ status: 201, message: 'ad created Sucessfully !' }))

            .catch(err => {

                if (err.code == 11000) {

                    reject({ status: 409, message: 'failed to create Ad !' });

                } else {

                    reject({ status: 500, message: 'Internal Server Error !' });
                }
            });
    });

exports.GetAds = user =>

    new Promise((resolve, reject) => {

        ad.find({})
            .then(ads => {
                resolve({ ads })
            })
            .catch(err => reject({ status: 500, message: 'Internal Server Error !' }));
    });
