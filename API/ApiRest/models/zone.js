const mongoose = require("mongoose");
const zoneSchemma = mongoose.Schema({
    id_client : {
        type:String,
        required: true
    },
    num_tables : {
        type:Number,
        required: true
    },

    zone_status : {
        type:Boolean,
        required:false
    }
});
module.exports = mongoose.model('Zone',zoneSchemma);