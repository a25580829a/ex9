using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.Json;


namespace ex9
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public class student777
    {
        public string StudentName { get; set; }

        public string StudentID { get; set; }

    }
    public partial class MainWindow : Window
    {
        List<student> students = new List<student>();
        List<Teacher> Teachers = new List<Teacher>();
        List<Course> courses = new List<Course>();
        List<Record> records = new List<Record>();

        student selectedStudent = null;
        Teacher selectedTeacher = null;
        Course selectedCourse = null;
        Record selectedRecord = null;
        public MainWindow()
        {
            InitializeComponent();
            student student1 = new student() { StudentID = "4A827013", StudentName = "林政翰" };
            students.Add(student1);

            cmb.ItemsSource = students;
            cmb.SelectedIndex = 0;

            //string fileName = "students2022.json";
            //string jsonString = File.ReadAllText(fileName);
            //student777 student2 = JsonSerializer.Deserialize<student777>(jsonString);
            //students.Add(student1);


            //StreamReader r = new StreamReader("students2022.json");
            //string jsonString = r.ReadToEnd();
            //student m = JsonConvert.DeserializeObject<student>(jsonString);

            string fileName = "students2022.json";
            string jsonString = File.ReadAllText(fileName);
            var list = JsonSerializer.Deserialize<List<student>>(jsonString);
            cmb.ItemsSource = list;
            cmb.SelectedIndex = 0;

            Teacher teacher1 = new Teacher() { TeacherName = "陳定宏"};
            //Course course1 = new Course(teacher1) { CourseName = "視窗程式設計", OpeningClass = "資工二甲", Point = 3, Type = "選修" };
            //teacher1.TeachingCourses.Add(course1);

            teacher1.TeachingCourses.Add(new Course(teacher1) { CourseName = "視窗程式設計", OpeningClass = "資工二甲", Point = 3, Type = "選修" });
            teacher1.TeachingCourses.Add(new Course(teacher1) { CourseName = "物件導向程式設計(A)", OpeningClass = "資工一甲", Point = 3, Type = "必修" });
            teacher1.TeachingCourses.Add(new Course(teacher1) { CourseName = "物件導向程式設計(B)", OpeningClass = "資工一甲", Point = 3, Type = "必修" });
            Teachers.Add(teacher1);
            Teacher teacher2 = new Teacher() { TeacherName = "李博明" };
            //Course course1 = new Course(teacher1) { CourseName = "視窗程式設計", OpeningClass = "資工二甲", Point = 3, Type = "選修" };
            //teacher1.TeachingCourses.Add(course1);

            teacher2.TeachingCourses.Add(new Course(teacher1) { CourseName = "UNIX/Linux作業系統實務", OpeningClass = "電子一丙", Point = 3, Type = "選修" });
            teacher2.TeachingCourses.Add(new Course(teacher1) { CourseName = "3D建模及快速成型實務", OpeningClass = "電子四丁", Point = 3, Type = "選修" });
            teacher2.TeachingCourses.Add(new Course(teacher1) { CourseName = "EDA設計流程整合", OpeningClass = "博研電子一甲", Point = 3, Type = "選修" });
            Teachers.Add(teacher2);

            Teacher teacher3 = new Teacher() { TeacherName = "賴培淋" };
            //Course course1 = new Course(teacher1) { CourseName = "視窗程式設計", OpeningClass = "資工二甲", Point = 3, Type = "選修" };
            //teacher1.TeachingCourses.Add(course1);

            teacher3.TeachingCourses.Add(new Course(teacher1) { CourseName = "雲端資料庫實務", OpeningClass = "電子四甲", Point = 3, Type = "選修" });
            teacher3.TeachingCourses.Add(new Course(teacher1) { CourseName = "工程‧倫理與社會", OpeningClass = "電子三甲", Point = 3, Type = "必修" });
            teacher3.TeachingCourses.Add(new Course(teacher1) { CourseName = "動態網頁設計", OpeningClass = "電子三甲", Point = 3, Type = "選修" });
            Teachers.Add(teacher3);


            TcsTreeV.ItemsSource = Teachers;

            foreach (Teacher teacher in Teachers)
            {
                foreach (Course course in teacher.TeachingCourses)
                {
                    courses.Add(course);
                }
            }
            lbCourses.ItemsSource = courses;
        }

        private void TcsTreeV_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (TcsTreeV.SelectedItem is Course)
            {
                selectedCourse = TcsTreeV.SelectedItem as Course;
                selectedTeacher = selectedCourse.Tutor;

                StatusLabel.Content = $"{selectedTeacher.TeacherName}: {selectedCourse.CourseName} ({selectedCourse.OpeningClass})";
            }
        }

        private void cmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedStudent = (student)cmb.SelectedItem;
            StatusLabel.Content = $"選課學生:{selectedStudent}";
        }

        private void lbCourses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedCourse = lbCourses.SelectedItem as Course;
            selectedTeacher = selectedCourse.Tutor;

            StatusLabel.Content = $"{selectedTeacher.TeacherName}: {selectedCourse.CourseName} ({selectedCourse.OpeningClass})";
        }

        private void selectButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedCourse != null && selectedStudent != null)
            {
                Record currentRecord = new Record()
                {
                    SelectedCourse = selectedCourse,
                    SelectedStudent = selectedStudent,
                };
                foreach (Record r in records)
                {
                    if (r.Equals(currentRecord))
                    {
                        MessageBox.Show($"{selectedStudent.StudentName} 已經選過 {selectedCourse.CourseName} 了，請選擇其他課程。");
                        return;
                    }
                }
                records.Add(currentRecord);
                recordListView.ItemsSource = records;
                recordListView.Items.Refresh();
            }
            else
            {
                MessageBox.Show("請選擇學生或課程");
            }
        }

        private void recordListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedRecord = recordListView.SelectedItem as Record;
            if (selectedRecord != null)
            {
                StatusLabel.Content = $"{selectedRecord.SelectedStudent.StudentName} : {selectedRecord.SelectedCourse.CourseName} 的選課紀錄";
            }
        }

        private void dropButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedRecord != null)
            {
                records.Remove(selectedRecord);
                recordListView.ItemsSource = records;
                recordListView.Items.Refresh();
            }
        }

        private void JSONexportButton_Click(object sender, RoutedEventArgs e)
        {
            
            if (selectedCourse != null && selectedStudent != null)
            {
                Record currentRecord = new Record()
                {
                    SelectedCourse = selectedCourse,
                    SelectedStudent = selectedStudent,
                };      
                records.Add(currentRecord);
                recordListView.ItemsSource = records;
                recordListView.Items.Refresh();
                
                string fileName = "records2022.json";
                var jsonString = JsonSerializer.Serialize(selectedRecord);
                File.WriteAllText(fileName, jsonString);
                Console.WriteLine(File.ReadAllText(fileName));
            }
            else
            {
                MessageBox.Show("請選擇學生或課程");
            }

        }
    }
}
