const config = require('../config/config');
const models = require('../models');

module.exports = {
    get: {
        home: (req, res, next) => {    
                res.render('homePage.hbs');
        }
    },
};