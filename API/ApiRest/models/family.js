const mongoose = require("mongoose");

const familySchema = mongoose.Schema({
    name:{
        type:String,
        required:true
    },
    id_client:{
        type: String,
        required: true
    },
})
module.exports = mongoose.model('Family', familySchema);