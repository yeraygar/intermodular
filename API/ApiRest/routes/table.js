const express = require("express");
const router = express.Router();
const tableSchema = require("../models/table");

//Create new User
router.post("/tables", (req, res) => {
    const table = tableSchema(req.body);
    table
        .save()
        .then((data) =>{
            res.json(data);
            console.log(`\nNew Table: \n ${data}`);
        })
        .catch((err) => {
            res.json({message:err});
            console.error(`Error get /api/table : ${err}`);
        })
})

//Get all Users
router.get("/tables", (req, res) => {
    userSchema
        .find()
        .then((data) =>{
            res.json(data);
            console.log(`\nAll Users: \n ${data}`);
        })
        .catch((err) => {
            res.json({message:err});
            console.log(`Error get /api/tables : ${err}`);
        })
})

//Get specific user
router.get("/tables/:id", (req, res) => {
    const {id} = req.params;
    userSchema
        .findById(id)
        .then((data) =>{
            res.json(data);
            console.log(`\nUser: \n ${data}`);
        })
        .catch((err) => {
            res.json({message:err});
            console.log(`Error get /api/tables/${id} : ${err}`);
        })
})

//Update user, si algun campo no se pone no se elimina
router.put("/tables/:id", (req, res) => {
    const {id} = req.params;
    const {name, email} = req.body;
    userSchema
        .updateOne({_id: id}, {$set:{name, email}})
        .then((data) =>{
            res.json(data);
            console.log(`\nUpdate succesful: \n ${data}`);
        })
        .catch((err) => {
            res.json({message:err});
            console.log(`Error update : ${err}`);
        })
})

//Delete User
router.delete("/tables/:id", (req, res) => {
    const {id} = req.params;
    userSchema
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