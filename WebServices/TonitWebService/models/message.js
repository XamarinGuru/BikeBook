'use strict';

const mongoose = require('mongoose');

const Schema = mongoose.Schema;

const messageSchema = mongoose.Schema({ 

	from 			: String,
    to              : String,
    content         : String,
    image           : String, 
    created_at      : String
});

mongoose.Promise = global.Promise;

module.exports = mongoose.model('message', messageSchema);     