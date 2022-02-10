const express = require("express");
const router = express.Router();
const prodImageSchema = require("../models/prodImage")

router.post("/prodImage",(req,res) =>{
    const prodImage = prodImageSchema(req.body);
    prodImage
    .save()
    .then((data) =>{
        res.json(data);
        console.log("Imagen subida");
    })
    .catch((err) =>{
        res.json({message:err});
        console.error(`error al subir la imagen: ${err}`);
    })
})

module.exports = router;