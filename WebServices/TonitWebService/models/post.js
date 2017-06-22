'use strict';

const mongoose = require('mongoose');

const Schema = mongoose.Schema;

const classifiedAdSchema = mongoose.Schema({

    owner: String,
    picture: String,
    content: String,
    created_at: String
});

mongoose.Promise = global.Promise;

module.exports = mongoose.model('post', classifiedAdSchema);