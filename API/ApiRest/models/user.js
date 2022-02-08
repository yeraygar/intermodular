const mongoose = require("mongoose");
const userSchema = mongoose.Schema({
    name:{
        type:String,
        required:true
    },
    email:{
        type:String,
        required:false
    },
    passw:{
        type:String,
        required:true
    },
    id_client:{
        type:String,
        required: true
    },
    rol:{ //Standard o Admin
        type:String,
        required: false,
        default: "Standard"
    },
    active:{
        type:Boolean,
        required:false,
        default:false
    }

})
module.exports = mongoose.model('User', userSchema);