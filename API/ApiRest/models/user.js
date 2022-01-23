const mongoose = require("mongoose");
const userSchema = mongoose.Schema({
    name:{
        type:String,
        required:true
    },
    email:{
        type:String,
        required:true
    },
    passw:{
        type:String,
        required:true
    },
    id_client:{
        type:String,
        required: true
    },
    rol:{
        type:String,
        required: false,
        default: "Empleado"
    },
    active:{
        type:Boolean,
        required:false,
        default:false
    }

})
module.exports = mongoose.model('User', userSchema);