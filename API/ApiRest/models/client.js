const mongoose = require("mongoose");
const clientSchema = mongoose.Schema({
    email:{
        type:String,
        required:true
    },
    passw:{
        type:String,
        required:true
    }
})
module.exports = mongoose.model('Client', clientSchema);