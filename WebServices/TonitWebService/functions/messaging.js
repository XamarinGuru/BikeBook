'use strict';

const message = require('../models/message')
const bcrypt = require('bcryptjs');

exports.sendMessage = (sender, reciever, text, image) => 

    new Promise((resolve, reject) => {

        const newMessage = new message({
            from: sender,
            to: reciever,
            content: text,
            image: image,
            created_at: new Date()
        })
        newMessage.save()

        .then(() => resolve({ status: 201, message: 'SentMessage Sucessfully !' }))

        .catch(err => {

            if (err.code == 11000) {

                reject({ status: 409, message: 'failed to send message !' });

            } else {

                reject({ status: 500, message: 'Internal Server Error !' });
            }
        });
    });

exports.GetMessages = user =>

    new Promise((resolve, reject) => {

        message.find({ $or: [{ to: user }, { from: user }] })
            .then(messages => {
                resolve({ messages })
            })
            .catch(err => reject({ status: 500, message: 'Internal Server Error !' }));
    });
