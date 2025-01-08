import { IAllergyEntryPersistence } from '../../dataschema/IAllergyEntryPersistence';
import { IAllergyPersistence } from '../../dataschema/IAllergyPersistence';
import mongoose from 'mongoose';

const AllergyEntrySchema = new mongoose.Schema(
    {
        domainId: { type: String, unique: true },
        entryNumber: { type: String, unique: true },
        medicalRecordNumber: { type: String, unique: true },
        allergy: { type: String, unique: true },
        description: { type: String, unique: false, required: false }
    },
    {
        timestamps: true
    }
);

export default mongoose.model<IAllergyEntryPersistence & mongoose.Document>('AllergyEntry', AllergyEntrySchema);
