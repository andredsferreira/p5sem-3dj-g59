import { IAllergyPersistence } from '../../dataschema/IAllergyPersistence';
import mongoose from 'mongoose';

const AllergySchema = new mongoose.Schema(
    {
        domainId: { type: String, unique: true },
        code: { type: String, unique: true },
        name: { type: String, unique: true },
        description: { type: String, unique: false, required: false }
    },
    {
        timestamps: true
    }
);

export default mongoose.model<IAllergyPersistence & mongoose.Document>('Allergy', AllergySchema);
