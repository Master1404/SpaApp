// Connect to the sheliapp database
db = db.getSiblingDB('SpaApp');

// First, let's remove all data to start fresh
db.comments.deleteMany({});
db.users.deleteMany({});


// Insert mock users
db.users.insertMany([
  {
    username: 'john_doe',
    email: 'john.doe@example.com',
    password: '$2a$11$u7Ob3tQbV1fM3LOCjxQ2jeIoZLDmz4KSLFn1.er7YG8tTLqJ5oQtO',//test123
  },
  {
    username: 'jane_smith',
    email: 'jane.smith@example.com',
    password: 'hashedpassword2',
  }
]);

print("users inserted successfully.");

print("comment states inserted successfully.");

db.comments.insertMany([
  {
    UserName: "john_doe",
    Email: "Siamese@gmail.com",
    HomePage: "Small",
    Text: "url_to_default_cat_image",
    TimeCreatedAt: "22:00",
  },
  {
    UserName: "Vova",
    Email: "Siamese@gmail.com",
    HomePage: "Small",
    Text: "url_to_default_cat_image",
    TimeCreatedAt: "22:00",
  }
]);
