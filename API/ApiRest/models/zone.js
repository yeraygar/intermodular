const mongoose = require("mongoose");
const zoneSchemma = mongoose.Schema({
    id_client : {
        type:String,
        required:true
    },
    
    zone_name : {
        type:String,
        required:true
    }
});
module.exports = mongoose.model('Zone',zoneSchemma);