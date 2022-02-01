const mongoose = require("mongoose");
const clientSchema = mongoose.Schema({
    name:{
        type: String,
        required: false
    },
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