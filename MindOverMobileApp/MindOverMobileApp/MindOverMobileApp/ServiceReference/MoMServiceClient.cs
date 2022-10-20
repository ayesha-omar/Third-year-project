using MindOverMobileApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MindOverMobileApp.ServiceReference
{
   public class MoMServiceClient
    {

        public User loginUser(string username, string password)
        {
            string json = getJson("Login/loginUser?username=" + username + "&password=" + password); //Login User, and get the user logged in
            if (json == "false")
            {

                return null;
            }
            User user = JsonConvert.DeserializeObject<User>(json); //deserialize to User class
            return user;
        }

        /* public bool EditStudent(int studentNum, string name, string surname, string email)
         {
             StudentClass student = new StudentClass
             {
                 studentNumber = studentNum,
                 Name=name,
                 Surname=surname,
                 email=email,
             };

             string jsonout = JsonConvert.SerializeObject(student);
             string result = uploadjson("Student",jsonout);


         }*/

        public bool ActivateCounsellor()
        {
            return false;
        }

        public bool ActivateExercise()
        {
            return false;
        }

        public bool addExercise(int id, string name, string description, char status, char active)
        {
            ExerciseClass exercise = new ExerciseClass
            {
                Name = name,
                description = description,
                Status = status,
                isActive = active
            };
            string json = JsonConvert.SerializeObject(exercise);
            string result = uploadjson("Exercise/addExercise?exercise=", json);
            if (result == "true")
            {
                return true;
            }
            return false;

        }

        public bool EditExercise(int id, string name, string decription, char status, char active)
        {
            ExerciseClass exercise = new ExerciseClass
            {
                Ex_id = id,
                Name = name,
                description = decription,
                Status = status,
                isActive = active
            };
            string json = JsonConvert.SerializeObject(exercise);
            string result = uploadjson("Exercise/EditExercise?exerciseClass=", json);
            if (result == "true")
            {
                return true;
            }
            return false;
        }


        public bool prescribeExercise(int counsID, int studentID, int exerciseID, DateTime dateprescribed)
        {

            PrescribeExerciseClass exercise = new PrescribeExerciseClass
            {
                Couns_id = counsID,
                Student_Num = studentID,
                Ex_id = exerciseID,
                DatePrescribed = dateprescribed
            };
            string json = JsonConvert.SerializeObject(exercise);
            string result = uploadjson("Exercise/Exercise/prescribeExercise?prescribeExercise=", json);
            if (result == "true")
            {
                return true;
            }
            return false;
        }

        public bool UnPrescribeExercise(int counsID, int exerciseID, int studentID)
        {
            PrescribeExerciseClass exercise = new PrescribeExerciseClass
            {
                Couns_id = counsID,
                Student_Num = studentID,
                Ex_id = exerciseID
            };
            string json = JsonConvert.SerializeObject(exercise);
            string result = uploadjson("Exercise/Exercise/UnPrescribeExercise?prescribeExercise=", json);
            if (result == "true")
            {
                return true;
            }
            return false;
        }

        public bool exerciseCompleted(int counsID, int exerciseID, int studentID)
        {

            PrescribeExerciseClass exercise = new PrescribeExerciseClass
            {
                Couns_id = counsID,
                Student_Num = studentID,
                Ex_id = exerciseID
            };
            string json = JsonConvert.SerializeObject(exercise);
            string result = uploadjson("Exercise/Exercise/exerciseCompleted?prescribeExercise=", json);
            if (result == "true")
            {
                return true;
            }
            return false;
        }

        public List<PrescribeExerciseClass> getPrescribedExercises(int studentNum)
        {
            string json = getJson("Exercise/getStudentPrescribedExercises?studentNum=" + studentNum);
            if (json == "false")
            {

                return null;
            }
            List<PrescribeExerciseClass> prescribeExerciseClasses = JsonConvert.DeserializeObject<List<PrescribeExerciseClass>>(json);
            return prescribeExerciseClasses;
        }

        public List<PrescribeExerciseClass> getAllPrescribedExercises()
        {
            string json = getJson("Exercise/getAllPrescribedExercises");
            if (json == "false")
            {

                return null;
            }
            List<PrescribeExerciseClass> prescribeExerciseClasses = JsonConvert.DeserializeObject<List<PrescribeExerciseClass>>(json);
            return prescribeExerciseClasses;
        }

        public CounsellorClass GetCounsellor(int Couns_id)
        {
            string json = getJson("Counsellor/GetCounsellor?Couns_id=" + Couns_id);
            if (json == "false")
            {

                return null;
            }
            CounsellorClass counsellor = JsonConvert.DeserializeObject<CounsellorClass>(json);
            return counsellor;
        }

        public List<CounsellorClass> GetCounsellors()
        {
            string json = getJson("Counsellor/GetCounsellors");
            if (json == "false")
            {

                return null;
            }
            List<CounsellorClass> counsellorClasses = JsonConvert.DeserializeObject<List<CounsellorClass>>(json);
            return counsellorClasses;

        }

        public StudentClass GetStudent(int studentNum)
        {
            string json = getJson("Student/GetStudent?Studentnum=" + studentNum);
            if (json == "false")
            {

                return null;
            }
            StudentClass student = JsonConvert.DeserializeObject<StudentClass>(json);
            return student;
        }

        public List<StudentClass> GetStudents()
        {
            string json = getJson("Student/GetStudents");
            if (json == "false")
            {
                return null;
            }
            List<StudentClass> students = JsonConvert.DeserializeObject<List<StudentClass>>(json);
            return students;

        }

        public List<StudentClass> GetCounsellorStudents(int counsellorID)
        {
            string json = getJson("Counsellor/GetCounsellorStudents?counsID=" + counsellorID);
            if (json == "false")
            {
                return null;
            }
            List<StudentClass> students = JsonConvert.DeserializeObject<List<StudentClass>>(json);
            return students;
        }

        public AdminClass GetAdmin(int Adminid)
        {
            string json = getJson("Student/getAdmin?Adminid=" + Adminid);
            if (json == "false")
            {

                return null;
            }
            AdminClass Admin = JsonConvert.DeserializeObject<AdminClass>(json);
            return Admin;
        }

        public ExerciseClass GetExercise(int exerciseid)
        {
            string json = getJson("Exercise/GetExercise?exerciseid=" + exerciseid);
            if (json == "false")
            {

                return null;
            }
            ExerciseClass exercise = JsonConvert.DeserializeObject<ExerciseClass>(json);
            return exercise;
        }

        public List<ExerciseClass> GetExercises()
        {
            string json = getJson("Exercise/GetExercises");
            if (json == "false")
            {

                return null;
            }
            List<ExerciseClass> exerciseClasses = JsonConvert.DeserializeObject<List<ExerciseClass>>(json);
            return exerciseClasses;

        }

        public List<BookConsultationClass> getBookings() //getAll Bookings
        {
            string json = getJson("Consultation/getBookings");
            if (json == "false")
            {

                return null;
            }
            List<BookConsultationClass> bookConsultationClasses = JsonConvert.DeserializeObject<List<BookConsultationClass>>(json);
            return bookConsultationClasses;
        }

        public List<BookConsultationClass> getCounsBookings(int counsID) //getBookingsByCounsellor Id
        {
            string json = getJson("Consultation/getCounsBookings?counsID=" + counsID);
            if (json == "false")
            {

                return null;
            }
            List<BookConsultationClass> bookConsultationClasses = JsonConvert.DeserializeObject<List<BookConsultationClass>>(json);
            return bookConsultationClasses;
        }
        public List<BookConsultationClass> getStudentBookings(int studentnum) //getBookings by Student ID
        {
            string json = getJson("Consultation/getBookingsByStudent?studentid=" + studentnum);
            if (json == "false")
            {

                return null;
            }
            List<BookConsultationClass> bookConsultationClasses = JsonConvert.DeserializeObject<List<BookConsultationClass>>(json);
            return bookConsultationClasses;

        }
        public BookConsultationClass getBooking(int id)
        {

            string json = getJson("Consultation/getBooking?id=" + id);
            if (json == "false")
            {

                return null;
            }
            BookConsultationClass bookConsultationClasses = JsonConvert.DeserializeObject<BookConsultationClass>(json);
            return bookConsultationClasses;
        }

        public bool book_Consultation(DateTime date, string time, int Stu_Num, int Couns_id)
        {
            BookConsultationClass bookConsultationClass = new BookConsultationClass
            {
                date = date,
                time = time,
                studentNum = Stu_Num,
                Couns_id = Couns_id
            };

            string json = JsonConvert.SerializeObject(bookConsultationClass);
            string result = uploadjson("Consultation/book_Consultation?book=", json);
            if (result == "true")
            {
                return true;
            }
            return false;
        }

        public List<DiaryClass> GetDiaryEntries()
        {
            string json = getJson("Diary/GetDiaryEntries");
            if (json == "false")
            {

                return null;
            }
            List<DiaryClass> diaryClasses = JsonConvert.DeserializeObject<List<DiaryClass>>(json);
            return diaryClasses;
        }

        public List<DiaryClass> GetStudentDiaryEntries(int studentnum)
        {
            string json = getJson("Diary/GetDiaryEntries?studentnum=" + studentnum);
            if (json == "false")
            {
                return null;
            }
            List<DiaryClass> diaryClasses = JsonConvert.DeserializeObject<List<DiaryClass>>(json);
            return diaryClasses;

        }

        public bool logDiaryEntry(int stuNum, string title, string body, DateTime date)
        {
            DiaryClass diaryClass = new DiaryClass
            {
                student_Num = stuNum,
                title = title,
                description = body,
                date = date
            };
            string json = JsonConvert.SerializeObject(diaryClass);
            string result = uploadjson("Diary/LogDiaryEntry?diaryClass=", json);
            if (result == "true")
            {
                return true;
            }
            return false;
        }

        public DiaryClass getDiaryEntry(int id)
        {
            string json = getJson("Diary/getDiaryEntry?id=" + id);
            if (json == "false")
            {
                return null;
            }
            DiaryClass diaryClass = JsonConvert.DeserializeObject<DiaryClass>(json);
            return diaryClass;

        }

        public bool EditDiaryEntry(int diaryid, string title, string description)
        {
            DiaryClass diaryClass = new DiaryClass
            {
                Diary_id = diaryid,
                title = title,
                description = description,
            };
            string json = JsonConvert.SerializeObject(diaryClass);
            string result = uploadjson("Diary/EditDiaryEntry?diaryClass=", json);
            if (result == "true")
            {
                return true;
            }
            return false;
        }

        /* [OperationContract]
         List<CallLog> GetCallLogs();*/
        public bool allocateSlot(int UserID, DateTime date, string timeName)
        {
            AvailableDatesAndTimesClass availableDatesAndTimesClass = new AvailableDatesAndTimesClass
            {
                coundID = UserID,
                Date = date,
                Time = timeName
            };
            string json = JsonConvert.SerializeObject(availableDatesAndTimesClass);
            string result = uploadjson("DatesTimes/allocateSlot?avDandT=", json);
            if (result == "true")
            {
                return true;
            }
            return false;

        }

        public bool deactivateSlot(int userID, DateTime date, string timeName)
        {
            AvailableDatesAndTimesClass availableDatesAndTimesClass = new AvailableDatesAndTimesClass
            {
                coundID = userID,
                Date = date,
                Time = timeName
            };
            string json = JsonConvert.SerializeObject(availableDatesAndTimesClass);
            string result = uploadjson("DatesTimes/deactivateSlot?avDandT=", json);
            if (result == "true")
            {
                return true;
            }
            return false;

        }

        //validates whether the name of the day is valid
        /*      public string getdayIDName(string name)
              { 

              }
              //validates whether the time is valid
              public string getTimeIDByName(string name);*/
        public StuCounsLinkClass getLinkInfoByStudent(int stunum)
        {
            string json = getJson("StuCounsLink/getLinkInfoByStudent?stunum=" + stunum);
            if (json == "false")
            {
                return null;
            }
            StuCounsLinkClass stuCounsLinkClass = JsonConvert.DeserializeObject<StuCounsLinkClass>(json);
            return stuCounsLinkClass;
        }

        //public int getMinCounsellor();

        public bool isSlotAvailable(DateTime date, string time)
        {
            AvailableDatesAndTimesClass availableDatesAndTimesClass = new AvailableDatesAndTimesClass
            {
                Date = date,
                Time = time
            };
            string json = JsonConvert.SerializeObject(availableDatesAndTimesClass);
            string result = getJson("DatesTimes/isSlotAvailable?avDandT=" + json);
            if (result == "false")
            {
                return false;
            }
            return true;
        }

        public List<AvailableDatesAndTimesClass> getUnAvailableSlots(int counsID)
        {
            string json = getJson("DatesTimes/getUnAvailableSlots?counsID=" + counsID);
            if (json == "false")
            {
                return null;
            }
            List<AvailableDatesAndTimesClass> availableDatesandtimes = JsonConvert.DeserializeObject<List<AvailableDatesAndTimesClass>>(json);
            return availableDatesandtimes;
        }
        public bool LinkStudentToCounsellor(int studentID, int CounsID)
        {
            StuAndCounsIDPair pair = new StuAndCounsIDPair
            {
                StudentNum = studentID,
                CounsID = CounsID
            };
            string json = JsonConvert.SerializeObject(pair);
            string result = uploadjson("StuCounsLink/LinkStudentToCounsellor?stuAndCounsIDPair=", json);
            if (result == "true")
            {
                return true;
            }
            return false;

        }
        public Boolean UnLinStudentFromCounsellork(int studentID, int CounsID)
        {
            StuAndCounsIDPair pair = new StuAndCounsIDPair
            {
                StudentNum = studentID,
                CounsID = CounsID
            };
            string json = JsonConvert.SerializeObject(pair);
            string result = uploadjson("StuCounsLink/UnLinStudentFromCounsellork?stuAndCounsIDPair=", json);
            if (result == "true")
            {
                return true;
            }
            return false;
        }


        public List<String> getTimesPerDay() {
            string result = getJson("DatesTimes/getTimesPerDay");

            if (result != "false")
            {
                
                List<String> timesperDay = JsonConvert.DeserializeObject<List<String>>(result);
                return timesperDay;
            }
            return null;
           
        }

        public NotificationClass GetNotification(int studentNum)
        {
            string result = getJson("Notification/GetNotification?studentNum="+ studentNum);
            if (result=="false") 
            {
                return null;
            }
            NotificationClass notification = JsonConvert.DeserializeObject<NotificationClass>(result);
            return notification;

        }

        public bool AddNotification(string description) 
        {
            NotificationClass notification = new NotificationClass
            {
                description =description
            };
            string jsonOut = JsonConvert.SerializeObject(notification);
            string result = uploadjson("Notification/AddNotification?notification=",jsonOut);
            if (result=="false") {
                return false;
            }
            return true;
        }

        public bool DeActivateNotification(int notificID) 
        {
            string result = getJson("Notification/DeActivateNotification?notificID="+notificID);
            if (result=="false") {
                return false;
            }
            return true;
        }

        public List<NotificationClass> getAllNotifications() 
        {
            string result = getJson("Notification/GetAllNotifications");
            List<NotificationClass> notificationClasses = JsonConvert.DeserializeObject<List<NotificationClass>>(result);
            return notificationClasses;

        
        }

        public MedicineTrackerClass GetMedicine(int medicineID)
        {
            string jsonIn = getJson("Medicine/GetMedicine?medicineID=" + medicineID);
            if (jsonIn == "false")
            {
                return null;
            }
            MedicineTrackerClass medicineTrackerClass = JsonConvert.DeserializeObject<MedicineTrackerClass>(jsonIn);
            return medicineTrackerClass;
        }

        public List<MedicineTrackerClass> GetMedicines(int studentNum)
        {
            string jsonIn = getJson("Medicine/GetMedicines?studentNum=" + studentNum);
            if (jsonIn == "false")
            {
                return null;
            }
            List<MedicineTrackerClass> medicineTrackerClass = JsonConvert.DeserializeObject<List<MedicineTrackerClass>>(jsonIn);
            return medicineTrackerClass;
        }
        public bool LogMedicine(DateTime date, string NameOfMedicine, string category, int studentNum, string DoctorName)
        {
            MedicineTrackerClass medicine = new MedicineTrackerClass
            {
                date = date,
                NameOfMedicine = NameOfMedicine,
                category = category,
                StudentNum = studentNum,
                DoctorName = DoctorName,
            };

            string jsnout = JsonConvert.SerializeObject(medicine);
            string jsonIN = uploadjson("Medicine/LogMedicine?medicine=", jsnout);
            if (jsonIN == "false")
            {
                return false;
            }
            return true;

        }        
        
        public bool EditMedicine(int medicineID,DateTime date, string NameOfMedicine, string category, int studentNum, string DoctorName)
        {
            MedicineTrackerClass medicine = new MedicineTrackerClass
            {
                ID= medicineID,
                date = date,
                NameOfMedicine = NameOfMedicine,
                category = category,
                StudentNum = studentNum,
                DoctorName = DoctorName,
            };

            string jsnout = JsonConvert.SerializeObject(medicine);
            string jsonIN = uploadjson("Medicine/EditMedicine?medicine=", jsnout);
            if (jsonIN == "false")
            {
                return false;
            }
            return true;

        }  
        public bool RemoveMedicine(int medicineID)
        { 
            string jsonIN = getJson("Medicine/RemoveMedicine?medicineID=" + medicineID);
            if (jsonIN == "false")
            {
                return false;
            }
            return true;

        }

        public bool logMood(string emotion, int studentNum) 
        {
            MoodClass moodClass = new MoodClass
            {
                time = DateTime.Now.TimeOfDay.ToString(),
                date=DateTime.Now.Date,
                emotion=emotion,
                student_Num=studentNum,
            };
            String jsonout = JsonConvert.SerializeObject(moodClass);
            string jsonin = uploadjson("Mood/LogMood?mood=",jsonout);
            if (jsonin=="false") {
                return false;
            }
            return true;
        }

        public List<MoodClass> GetMoods() 
        {
            string jsonout = getJson("Mood/GetMoods");
            List<MoodClass> mood = JsonConvert.DeserializeObject<List<MoodClass>>(jsonout);
            if (jsonout=="false") 
            {
                return null;
            }
            return mood;
        }



        public MoodClass GetMood(int moodID)
        {
            string jsonout = getJson("Mood/getMood?id="+moodID);
            MoodClass mood = JsonConvert.DeserializeObject<MoodClass>(jsonout);
            if (jsonout == "false")
            {
                return null;
            }
            return mood;
        }
        //HELPER FUNCTIONS
        public string getJson(string myurl)
        {

            //TAKES in the url and returns the json for the requested JSON
            WebClient apiclient = new WebClient();
            apiclient.Headers["Content-type"] = "application/json";
            apiclient.Encoding = Encoding.UTF8;

            string json = apiclient.DownloadString(HelperClass.apiurl + myurl);
            return json;
        }

        public string uploadjson(string myurl, string inputJson)
        {
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(HelperClass.apiurl + myurl, inputJson);
            return json;
        }


    }
}
