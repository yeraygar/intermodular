const express = require("express");
const router = express.Router();
const familySchema = require("../models/family");

//Create new Family
router.post("/family", (req, res) => {
    const family = familySchema(req.body);
    family
        .save()
        .then((data) =>{
            res.json(data);
            console.log(`\nNew Family: \n ${data}`);
        })
        .catch((err) => {
            res.json({message:err});
            console.error(`Error get /api/family : ${err}`);
        })
})

//Get specific family
router.get("/family/:id", (req, res) => {
    const {id} = req.params;
    familySchema
        .findById(id)
        .then((data) =>{
            res.json(data);
            console.log(`\nfamily: \n ${data}`);
        })
        .catch((err) => {
            res.json({message:err});
            console.log(`Error get /api/family/${id} : ${err}`);
        })
})

//Update family, si algun campo no se pone no se elimina
router.put("/family/:id", (req, res) => {
    const {id} = req.params;
    const {name, email} = req.body;
    familySchema
        .updateOne({_id: id}, {$set:{name, email}})
        .then((data) =>{
            res.json(data);
            console.log(`\nFamily Update succesful: \n ${data}`);
        })
        .catch((err) => {
            res.json({message:err});
            console.log(`Family Error update : ${err}`);
        })
})

//Delete family
router.delete("/family/:id", (req, res) => { 
    const {id} = req.params;
    familySchema
        .remove({_id: id})
        .then((data) =>{
            res.json(data);
            console.log(`\nFamily Delete succesful: \n ${data}`);
        })
        .catch((err) => {
            res.json({message:err});
            console.log(`Family Error delete : ${err}`);
        })
})

//Get all family from a client
router.get("/family/client/:id_client", (req, res) => {
    const {id_client} = req.params;
    familySchema
        .find({id_client: id_client})
        .then((data) =>{
            res.json(data);
            console.log(`\nfamily: \n ${data}`);
        })
        .catch((err) => {
            res.json({message:err});
            console.log(`Error get /api/family/client/${id} : ${err}`);
        })
})






module.exports = router