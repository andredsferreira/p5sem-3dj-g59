import { Router } from 'express';
import auth from './routes/userRoute';
import user from './routes/userRoute';
import role from './routes/roleRoute';
import medCondition from './routes/medConditionRoute';
import allergy from "./routes/allergyRoute";
import allergyEntry from "./routes/allergyEntryRoute"
import medicalRecord from './routes/medicalRecordRoute';
import familyHistoryEntry from './routes/familyHistoryEntryRoute';
import medicalConditionEntry from './routes/medConditionEntryRoute';
import { AllergyEntry } from '../domain/allergyEntry';

export default () => {
	const app = Router();

	auth(app);
	user(app);
	role(app);
	medCondition(app);
	allergy(app);
	allergyEntry(app)
	medicalRecord(app);
	familyHistoryEntry(app);
	medicalConditionEntry(app);


	return app
}