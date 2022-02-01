const express = require("express");
const router = express.Router();
const userSchema = require("../models/user");

//Create new User
router.post("/users", (req, res) => {
    const user = userSchema(req.body);
    user
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

//Get all Users
router.get("/users", (req, res) => {
    userSchema
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

//Get specific user
router.get("/users/:id", (req, res) => {
    const {id} = req.params;
    userSchema
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

//Update user, si algun campo no se pone no se elimina
router.put("/users/:id", (req, res) => {
    const {id} = req.params;
    const {name, email, active} = req.body;
    userSchema
        .updateOne({_id: id}, {$set:{name, email, active}})
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
router.delete("/users/:id", (req, res) => { 
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

//Get all users from a client
router.get("/users/client/:id_client", (req, res) => {
    const {id_client} = req.params;
    userSchema
        .find({id_client: id_client})
        .then((data) =>{
            res.json(data);
            console.log(`\nUser: \n ${data}`);
        })
        .catch((err) => {
            res.json({message:err});
            console.log(`Error get /api/users/client/${id} : ${err}`);
        })
})

//Get all active users from a client
router.get("/users/client/:id_client/active", (req, res) => {
    const {id_client} = req.params;
    userSchema
        .find({id_client: id_client, active: true})
        .then((data) =>{
            res.json(data);
            console.log(`\nACTIVES: \n ${data}`);
        })
        .catch((err) => {
            res.json({message:err});
            console.log(`Error get /api/users/client/${id}/ : ${err}`);
        })
})

//Get all active users from a client
router.get("/users/client/:id_client/inactive", (req, res) => {
    const {id_client} = req.params;
    userSchema
        .find({id_client: id_client, active: false})
        .then((data) =>{
            res.json(data);
            console.log(`\nINACTIVES: \n ${data}`);
        })
        .catch((err) => {
            res.json({message:err});
            console.log(`Error get /api/users/client/${id}/ : ${err}`);
        })
})

//Get all Admins from a client
router.get("/users/client/:id_client/admin", (req, res) => {
    const {id_client} = req.params;
    userSchema
        .find({id_client: id_client, rol: "Admin"})
        .then((data) =>{
            res.json(data);
            console.log(`\nAdmins: \n ${data}`);
        })
        .catch((err) => {
            res.json({message:err});
            console.log(`Error get /api/users/client/${id}/ : ${err}`);
        })
})

module.exports = router;