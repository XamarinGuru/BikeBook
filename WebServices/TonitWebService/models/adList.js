'use strict';

const mongoose = require('mongoose');

const Schema = mongoose.Schema;

const adLisSchema = mongoose.Schema({

    db_Id: String,
    title: String,
    pictureThumbnail: String,
    city: String,
    province: String,
    created_at: String,
});

mongoose.Promise = global.Promise;

module.exports = mongoose.model('adList', classifiedAdSchema);        