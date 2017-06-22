'use strict';

const mongoose = require('mongoose');

const Schema = mongoose.Schema;

const classifiedAdSchema = mongoose.Schema({

    owner: String,
    title: String,
    picture: String,
    description: String,
    city: String,
    provence: String,
    created_at: String
});

mongoose.Promise = global.Promise;

module.exports = mongoose.model('classifiedAd', classifiedAdSchema);