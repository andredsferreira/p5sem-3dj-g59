import { IMedicalConditionEntryPersistence } from '../../dataschema/IMedicalConditionEntryPersistence';
import mongoose from 'mongoose';

const MedicalConditionEntrySchema = new mongoose.Schema(
    {
        domainId: { type: String, unique: true },
        entryNumber: { type: String, required: true, unique: true},
        medicalRecordNumber: { type: String, required: true},
        condition: { type: String, required: true },
        year: { type: Number, required: true },
    },
    {
        timestamps: true
    }
);

export default mongoose.model<IMedicalConditionEntryPersistence & mongoose.Document>('MedicalConditionEntry', MedicalConditionEntrySchema);
