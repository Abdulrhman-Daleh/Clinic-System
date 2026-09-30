These are the design and attributes.

Now I will write the relationships from both sides as (ordinality, cardinality) and why.

Now, for the lookup tables where only the title and ID are available, the relationship is like this:

From the lookup table to the linked table is (0, M) because every status can be given to 0 records or many different records.

From the other table to the lookup is (1, 1) because a record from that table is required to have at least 1 status and can have at most 1 status only. That's it for now. Time for the other things.

🏥 Database Relationships

👥 Staff and Basic Information

🗓️ [Schedules] ↔ [Staff]

From [Schedules] to [Staff]: (0, M) ordinality [0] means a schedule can be added without being linked to a staff member right away. Cardinality [M] means a schedule can be given to many different staff members.

From [Staff] to [Schedules]: (1, 1) ordinality [1] means a staff member must have at least one schedule. Cardinality [1] means a staff member can't have 2 different schedules; it's one, and we update it if needed.

🧑 [Persons] ↔ [Staff]

From [Persons] to [Staff]: (0, 1) ordinality [0] means a person can be something other than a staff member, and [1] if a person is a staff member, it's only [1] staff member.

From [Staff] to [Persons]: (1, 1) ordinality [1] means a staff member is linked to at least one person. Cardinality [1] means a staff member can't be linked to 2 persons; it's only one person.

🧪 Laboratory Tests

🧑‍⚕️ [Staff] ↔ [LabTests]

From [Staff] to [LabTests]: (0, M) ordinality [0] means a nurse can do 0 lab tests, and cardinality [M] means a nurse can have done many lab tests.

From [LabTests] to [Staff]: (1, 1) ordinality [1] means a lab test requires at least a nurse, and cardinality [1] means a lab test can be done by one nurse at max.

🛡️ Insurance

🧑‍💼 [Staff] ↔ [insurance]

From [Staff] to [insurance]: (0, M) ordinality [0] means a receptionist can have filled 0 insurance records, and cardinality [M] means a receptionist can fill many different insurance records.

From [insurance] to [Staff]: (1, 1) ordinality [1] means an insurance filling requires at least 1 receptionist, and cardinality [1] means an insurance record can't be filled by more than 1 receptionist, only one.

🧑 [Persons] ↔ [Patients]

From [Persons] to [Patients]: (0, 1) ordinality [0] means a person does not need to be a patient, and cardinality [1] means if a person is a patient, then it's only one patient.

From [Patients] to [Persons]: (1, 1) ordinality [1] means a patient is required to be assigned as a person, and cardinality [1] means a patient can't be more than one person, only a maximum of 1 person.

🛡️ [insurance] ↔ [Patients]

From [insurance] to [Patients]: (1, M) ordinality [1] means an insurance record must belong to a patient, and cardinality [M] means the same insurance record can be returned to more than one patient, like a patient and their family, depending on the type. They all take the same insurance.

From [Patients] to [insurance]: (0, 1) ordinality [0] means a patient might have no insurance at all, and cardinality [1] means if a patient has insurance, they can have a maximum of one insurance.

📞 Contact Information

🧑 [Persons] ↔ [ContactInformations]

From [Persons] to [ContactInformations]: (0, M) ordinality [0] means a person might have no contact information at all, and cardinality [M] means the person can have more than 1 contact information, like normal and emergency.

From [ContactInformations] to [Persons]: (1, 1) ordinality [1] means a contact information record must belong to one person, and cardinality [1] means a contact information record can't be given to many people; it's for one person at max.

📜 Policies

🧑‍💼 [Staff] ↔ [Policies]

From [Staff] to [Policies]: (1, M) ordinality [1] means the owner must add at least one policy, and cardinality [M] means the owner can add many policies later in the future.

From [Policies] to [Staff]: (1, 1) ordinality [1] means the policy is written by at least one staff member (owner), and cardinality [1] means the policy can't be written by more than one staff member, only the owner (and one owner only).

📅 Appointments

🧪 [LabTests] ↔ [Appointments]

From [LabTests] to [Appointments]: (1, 1) ordinality [1] means a lab test gets ordered by at least one appointment, and cardinality [1] means a lab test can't be for many appointments; it's for one appointment only.

From [Appointments] to [LabTests]: (0, M) ordinality [0] means an appointment might not have a lab test, and cardinality [M] means an appointment can have many different lab tests the patient must do.

🧑‍🤝‍🧑 [Patients] ↔ [Appointments]

From [Patients] to [Appointments]: (0, M) ordinality [0] means a patient, the moment they register, might not have any appointment, and cardinality [M] means a patient might have many appointments at available times.

From [Appointments] to [Patients]: (1, 1) ordinality [1] means an appointment is in the name of at least one patient, and cardinality [1] means an appointment can't be given to many different patients; it's for one patient only.

🧑‍⚕️ [Staff] ↔ [Appointments]

From [Staff] to [Appointments]: (0, M) ordinality [0] means a doctor might have no appointments at all, and cardinality [M] means a doctor might have many appointments.

From [Appointments] to [Staff]: (1, 1) ordinality [1] means an appointment is linked to at least one doctor, and cardinality [1] means an appointment can't be in the name of more than one doctor, even if a nurse sees the patient before the doctor.

💊 Prescriptions

📅 [Appointments] ↔ [Prescriptions]

From [Appointments] to [Prescriptions]: (0, 1) ordinality [0] means an appointment might have no prescription at all, and cardinality [1] means if an appointment has a prescription, it's only one prescription.

From [Prescriptions] to [Appointments]: (1, 1) ordinality [1] means a prescription is required to go back to an appointment, and cardinality [1] means the single prescription can't be for more than one appointment; it can't be for 2 appointments.

💊 [Medicines] ↔ [PrescriptionItems]

From [Medicines] to [PrescriptionItems]: (0, M) ordinality [0] means a medicine is not required to be a prescription item, and cardinality [M] means a medicine of the same type can be given to many prescription items, not the same one, but one piece of the medicine, because we could have like 1000 medicines in quantity under the same information.

From [PrescriptionItems] to [Medicines]: (1, 1) ordinality [1] means a single prescription item must have at least one medicine, and cardinality [1] means a single prescription item can have only one medicine in it, while the full prescription can have many items.

🧾 [Prescriptions] ↔ [PrescriptionItems]

From [Prescriptions] to [PrescriptionItems]: (1, M) ordinality [1] means a prescription must have at least one prescription item, and cardinality [M] means a single prescription can have more than one prescription item.

From [PrescriptionItems] to [Prescriptions]: (1, 1) ordinality [1] means that the whole item must be linked back to at least one prescription, and cardinality [1] means that whole item goes back to only one prescription, not the medicine inside the item, but the record that represents the item is for only one prescription at max.

🩺 Medical Records

🧑‍🤝‍🧑 [Patients] ↔ [MedicalRecords]

From [Patients] to [MedicalRecords]: (1, 1) ordinality [1] means a patient, when they get registered, must have at least one medical record, even if it's empty, and cardinality [1] means a patient can't have more than one medical record over time; it's only one medical record at max.

From [MedicalRecords] to [Patients]: (1, 1) ordinality [1] means a medical record must belong to one patient, and cardinality [1] means a medical record can't be for 2 patients, but only one patient.

🧑‍⚕️ [Staff] ↔ [SOAPNotes]

From [Staff] to [SOAPNotes]: (0, M) ordinality [0] means the doctor might have written 0 SOAP notes, and cardinality [M] means the doctor can write many SOAP notes.

From [SOAPNotes] to [Staff]: (1, 1) ordinality [1] means a SOAP note must belong to at least one doctor, and cardinality [1] means a SOAP note can't be written by or go back to more than one doctor; it's one doctor only.

📅 [Appointments] ↔ [SOAPNotes]

From [Appointments] to [SOAPNotes]: (0, 1) ordinality [0] means an appointment might not have any SOAP note, and one reason is it might get cancelled, and cardinality [1] means if an appointment does have one, it has only 1 SOAP note at max.

From [SOAPNotes] to [Appointments]: (1, 1) ordinality [1] means a SOAP note must belong to an appointment, and cardinality [1] means a SOAP note can't be for different appointments; it's for one appointment only.

📋 [MedicalRecords] ↔ [SOAPNotes]

From [MedicalRecords] to [SOAPNotes]: (0, M) ordinality [0] means a medical record might be created new and has no SOAP notes, and cardinality [M] means a medical record might have many SOAP notes over time.

From [SOAPNotes] to [MedicalRecords]: (1, 1) ordinality [1] means every single SOAP note must be stored in at least one medical record, and cardinality [1] means the same SOAP note can't be given to 2 different medical records; it's for a medical record only.

🔗 Referrals

🧑‍⚕️ [Staff] ↔ [Referrals]

From [Staff] to [Referrals]: (0, M) ordinality [0] means a doctor might have written no referrals, and cardinality [M] means a doctor can write many referrals.

From [Referrals] to [Staff]: (1, 1) ordinality [1] means a referral must be written by at least one doctor, and cardinality [1] means a referral can't be written by more than one doctor, but only one doctor.

📅 [Appointments] ↔ [Referrals]

From [Appointments] to [Referrals]: (0, 1) ordinality [0] means an appointment might have no referrals at all, and cardinality [1] means an appointment can have one referral at max.

From [Referrals] to [Appointments]: (1, 1) ordinality [1] means every referral must be for an appointment, and cardinality [1] means a referral can't be for more than one appointment; it's for one appointment only.

📝 [SOAPNotes] ↔ [Referrals]

From [SOAPNotes] to [Referrals]: (0, M) ordinality [0] means that, but every SOAP note is for a referral, and cardinality [M] means a single SOAP note might be given to more than one referral for that patient, but it has to be the same patient the SOAP note is for.

From [Referrals] to [SOAPNotes]: (1, 1) ordinality [1] means a referral must have at least one SOAP note for the specialist to get the context, and cardinality [1] means a referral can't have many SOAP notes, each one saying different things; only one SOAP note.

💰 Penalties and Payments

📅 [Appointments] ↔ [Penalties]

From [Appointments] to [Penalties]: (0, M) ordinality [0] means an appointment might have no penalty, and cardinality [M] means an appointment might have more than one penalty, for example, a no-show fee and a late payment fee.

From [Penalties] to [Appointments]: (1, 1) ordinality [1] means a penalty must belong to at least one appointment, and cardinality [1] means a penalty can't be for more than one appointment; it's for one only.

📜 [Policies] ↔ [Penalties]

From [Policies] to [Penalties]: (0, M) ordinality [0] means a policy might be given to no penalty, and cardinality [M] means a policy can be given to many penalties.

From [Penalties] to [Policies]: (1, 1) ordinality [1] means a penalty requires at least one policy, and cardinality [1] means a penalty can't be assigned more than one policy because how are we going to calculate the fees if it has more than one?

📅 [Appointments] ↔ [Payments]

From [Appointments] to [Payments]: (0, 1) ordinality [0] means an appointment might not have any payments; it might get cancelled, and cardinality [1] means an appointment can have at max 1 payment, and it's required if the appointment is over; there must be a payment, so appointments do not have partial payments.

From [Payments] to [Appointments]: (0, 1) ordinality [0] means a payment might not be for an appointment, and cardinality [1] means it can go back to only one appointment.

⚠️ [Penalties] ↔ [Payments]

From [Penalties] to [Payments]: (0, M) ordinality [0] means a penalty might still have no payments, and cardinality [M] means a penalty can have many payments.

From [Payments] to [Penalties]: (0, 1) ordinality [0] means a payment might be for something other than a penalty, and cardinality [1] means a payment can go back to at max one penalty.

🧑‍💼 [Staff] ↔ [Payments]

From [Staff] to [Payments]: (0, M) ordinality [0] means a receptionist can process no payments at all, and cardinality [M] means a receptionist can process many payments.

From [Payments] to [Staff]: (1, 1) ordinality [1] means a payment must be processed by at least one receptionist, and cardinality [1] means a payment can't be processed by more than one receptionist; only one.

🔔 Appointment Reminders

🧑‍💼 [Staff] ↔ [AppointmentReminders]

From [Staff] to [AppointmentReminders]: (0, M) ordinality [0] means a receptionist might send zero reminders, and cardinality [M] means a receptionist can send many reminders.

From [AppointmentReminders] to [Staff]: (1, 1) ordinality [1] means a reminder must be sent by at least one receptionist, and cardinality [1] means a reminder can't be sent by more than one receptionist.

📅 [Appointments] ↔ [AppointmentReminders]

From [Appointments] to [AppointmentReminders]: (0, M) ordinality [0] means an appointment can have no reminders yet, and cardinality [M] means an appointment can have many reminders.

From [AppointmentReminders] to [Appointments]: (1, 1) ordinality [1] means a reminder goes back to at least one appointment, and cardinality [1] means a single reminder can't be for more than one appointment.

🖼️ Images and Audit Trails

🧑 [Persons] ↔ [Images]

From [Persons] to [Images]: (0, M) ordinality [0] means a person can have no images, and cardinality [M] means a person can have many images.

From [Images] to [Persons]: (1, 1) ordinality [1] means an image must belong to at least one person, and cardinality [1] means an image can't be for more than one person; it's for a single person.

🧾 [Staff] ↔ [AuditTrails]

From [Staff] to [AuditTrails]: (0, M) ordinality [0] means a staff member might write no audit trails, and cardinality [M] means a staff member might write many audit trails.

From [AuditTrails] to [Staff]: (1, 1) ordinality [1] means an audit trail must be written by at least one staff member, and cardinality [1] means an audit trail can't be written by more than one staff member.
