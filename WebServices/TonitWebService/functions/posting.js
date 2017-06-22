'use strict';

const post = require('../models/post')

exports.createPost = (owner, picture, text) =>

    new Promise((resolve, reject) => {

        const newPost = new post({
            owner: owner,
            picture: picture,
            content: text,
            created_at: new Date()
        })
        newPost.save()

            .then(() => resolve({ status: 201, message: 'post Sucessfully !' }))

            .catch(err => {

                if (err.code == 11000) {

                    reject({ status: 409, message: 'failed to send message !' });

                } else {

                    reject({ status: 500, message: 'Internal Server Error !' });
                }
            });
    });

exports.GetPosts = user =>

    new Promise((resolve, reject) => {

        post.find({})
            .then(posts => {
                resolve({ posts })
            })
            .catch(err => reject({ status: 500, message: 'Internal Server Error !' }));
    });
