import dotenv from 'dotenv';

// Set the NODE_ENV to 'development' by default
process.env.NODE_ENV = process.env.NODE_ENV || 'development';

const envFound = dotenv.config();
if (!envFound) {
  // This error should crash whole process

  throw new Error("⚠️  Couldn't find .env file  ⚠️");
}

export default {
  /**
   * Your favorite port : optional change to 4000 by JRT
   */
  port: parseInt(process.env.PORT, 10) || 4000,

  /**
   * That long string from mlab
   */
  databaseURL: process.env.MONGODB_URI || "mongodb+srv://group059user:lSzxTrzFsqyEh9oS@arqsi3djg059cluster.um2mz.mongodb.net/medrecords?retryWrites=true&w=majority&appName=ARQSI3DJG059Cluster",

  /**
   * Your secret sauce
   */
  jwtSecret: process.env.JWT_SECRET || "my sakdfho2390asjod$%jl)!sdjas0i secret",

  /**
   * Used by winston logger
   */
  logs: {
    level: process.env.LOG_LEVEL || 'info',
  },

  /**
   * API configs
   */
  api: {
    prefix: '/api',
  },

  controllers: {
    role: {
      name: "RoleController",
      path: "../controllers/roleController"
    },
    medCondition: {
      name: "MedConditionController",
      path: "../controllers/medConditionController"
    },
    allergy: {
      name: "AllergyController",
      path: "../controllers/allergyController"
    },
    medicalRecord: {
      name: "MedicalRecordController",
      path: "../controllers/medicalRecordController"
    },
    familyHistoryEntry: {
      name: "FamilyHistoryEntryController",
      path: "../controllers/familyHistoryEntryController"
    },
  },

  repos: {
    role: {
      name: "RoleRepo",
      path: "../repos/roleRepo"
    },
    user: {
      name: "UserRepo",
      path: "../repos/userRepo"
    },
    medCondition: {
      name: "MedConditionRepo",
      path: "../repos/medConditionRepo"
    },
    allergy: {
      name: "AllergyRepo",
      path: "../repos/allergyRepo"
    },
    medicalRecord: {
      name: "MedicalRecordRepo",
      path: "../repos/medicalRecordRepo"
    },
    familyHistoryEntry: {
      name: "FamilyHistoryEntryRepo",
      path: "../repos/familyHistoryEntryRepo"
    },
  },

  services: {
    role: {
      name: "RoleService",
      path: "../services/roleService"
    },
    medCondition: {
      name: "MedConditionService",
      path: "../services/medConditionService"
    },
    allergy: {
      name: "AllergyService",
      path: "../services/allergyService"
    },
    medicalRecord: {
      name: "MedicalRecordService",
      path: "../services/medicalRecordService"
    },
    familyHistoryEntry: {
      name: "FamilyHistoryEntryService",
      path: "../services/familyHistoryEntryService"
    },
  },
};
