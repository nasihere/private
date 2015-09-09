var express = require('express');
var app = express();

var userRouter = express.Router();
// you need to set mergeParams: true on the router,
// if you want to access params from the parent router
var itemRouter = express.Router({mergeParams: true});

// you can nest routers by attaching them as middleware:
userRouter.use('/:userId/items', itemRouter);

userRouter.route('/')
    .get(function (req, res) {
      /*  res.status(200)
            .send('hello user');
	*/
            //res.json(null).json({ user: 'tobi' })
            res.status(200)
            .links({
				  next: 'http://api.example.com/users?page=2',
				  last: 'http://api.example.com/users?page=5'
				});
    });

userRouter.route('/:userId')
    .get(function (req, res) {
        res.status(200)
            .send('hello user ' + req.params.userId);
    });

itemRouter.route('/')
    .get(function (req, res) {
        res.status(200)
            .send('hello item from user ' + req.params.userId);
    });

itemRouter.route('/:itemId')
    .get(function (req, res) {
        res.status(200)
            .send('hello item ' + req.params.itemId + ' from user ' + req.params.userId);
    });

app.use('/user', userRouter);

app.listen(3003);
/*
//Output
Try: http://localhost:3003/user/123456/items/ABC
output : hello item ABC from user 123456
Try: http://localhost:3003/user/123456/items
output : hello item from user 123456
Try: http://localhost:3003/user/123456
output : hello user 123456
Try:http://localhost:3003/user
output:hello user*/

/*
	json resposne
	  res.status(200)
            .json({ user: 'tobi' });
*/