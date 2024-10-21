// Switch to the admin database
db = db.getSiblingDB('admin');

// Check if the admin user already exists
var adminUser = db.getUser("admin");
if (!adminUser) {
    // If the admin user doesn't exist, create it
    db.createUser({
        user: 'admin',
        pwd: 'admin123',
        roles: [
            {
                role: 'root',
                db: 'admin',
            },
        ],
    });
    print("Admin user created.");
} else {
    print("Admin user already exists.");
}

// Authenticate as the admin user
db.auth('admin', 'admin123');

// Switch to the sheliapp database
db = db.getSiblingDB('SpaApp');

db.createCollection('users', {
  validator: {
    $jsonSchema: {
      bsonType: 'object',
      required: ['username', 'email', 'password'],
      properties: {
        username: {
          bsonType: 'string',
          description: 'must be a string and is required',
        },
        email: {
          bsonType: 'string',
          description: 'must be a string and is required',
        },
        password: {
          bsonType: 'string',
          description: 'must be a string and is required',
        },
       
      },
    },
  },
});

db.createCollection('сomments', {
  validator: {
    $jsonSchema: {
      bsonType: 'object',
      required: ['owner_id', 'UserName', 'Email', 'HomePage', 'Text', 'TimeCreatedAt'],
      properties: {
        _id: {
          bsonType: 'objectId',
          description: 'must be an objectId and is required',
        },
        owner_id: {
          bsonType: 'objectId',
          description: 'must be an objectId and is required',
        },
        UserName: {
          bsonType: 'string',
          description: 'must be a string and is required',
        },
        Email: {
          bsonType: 'string',
          description: 'must be a string and is required',
        },
        HomePage: {
          bsonType: 'string',
        },
        Text: {
          bsonType: 'string',
          description: 'must be an int and is required'
        },
        TimeCreatedAt: {
          bsonType: 'string',
          description: 'must be a string and is required'
        },
        
      },
    },
  },
});

print("All collections created successfully.");