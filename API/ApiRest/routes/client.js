const express = require("express");
const router = express.Router();
const clientSchema = require("../models/client");

//Create new client
router.post("/client", (req, res) => {
    const client = clientSchema(req.body);
    client
        .save()
        .then((data) =>{
            res.json(data);
            console.log(`\nNew User: \n ${data}`);
        })
        .catch((err) => {
            res.json({message:err});
            console.error(`Error get /api/users : ${err}`);
        })
})

//Get all Clients
router.get("/client", (req, res) => {
    clientSchema
        .find()
        .then((data) =>{
            res.json(data);
            console.log(`\nAll Users: \n ${data}`);
        })
        .catch((err) => {
            res.json({message:err});
            console.log(`Error get /api/users : ${err}`);
        })
})

//Get specific Client
router.get("/client/:id", (req, res) => {
    const {id} = req.params;
    clientSchema
        .findById(id)
        .then((data) =>{
            res.json(data);
            console.log(`\nUser: \n ${data}`);
        })
        .catch((err) => {
            res.json({message:err});
            console.log(`Error get /api/users/${id} : ${err}`);
        })
})

//Check if client email already exists
router.get("/client/email/:email", (req, res) => {
    const {email} = req.params;
    clientSchema
        .find({email:email})
        .then((data) =>{
            if(data.length != [])res.json(true);
            else res.json(false)
            console.log(`\nExiste ya ${email} en la BBDD? : ${(data.length != [] ? "SI" : "NO")} existe`);
        })
        .catch((err) => {
            res.json({message:err});
            console.log(`Error get /client/${email} : ${err}`);
        })
})

//Check if email and password are correct
router.get("/client/validate/:email/:passw", (req, res) => {
    const email = req.params.email;
    const passw = req.params.passw;
    clientSchema
        .find({email:email, passw:passw})
        .then((data) =>{
            if(data.length != [])res.json(data);
            else res.status(500).json(data)
            console.log(`\nEmail: ${email} y Passw: ${passw} correctos? : ${(data.length != [] ? "SI" : "NO")}\n${data}`);
        })
        .catch((err) => {
            res.json({message:err});
            console.log(`Error get /client/${email} : ${err}`);
        })
})

//Update client si algun campo no se pone no se elimina
router.put("/client/:id", (req, res) => {
    const {id} = req.params;
    const {name, email, passw} = req.body;
    clientSchema
        .updateOne({_id: id}, {$set:{name, email, passw}})
        .then((data) =>{
            res.json(data);
            console.log(`\nUpdate succesful: \n ${data}`);
        })
        .catch((err) => {
            res.json({message:err});
            console.log(`Error update : ${err}`);
        })
})

//Delete client
router.delete("/client/:id", (req, res) => {
    const {id} = req.params;
    clientSchema
        .remove({_id: id})
        .then((data) =>{
            res.json(data);
            console.log(`\nDelete succesful: \n ${data}`);
        })
        .catch((err) => {
            res.json({message:err});
            console.log(`Error delete : ${err}`);
        })
})



module.exports = router;