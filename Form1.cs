using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace TaskManagerV2
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            ClearInput();
        }

        private void ClearInput()
        {
            textName.Text = string.Empty;
            textDesc.Text = string.Empty;
            dateStart.Value = DateTime.Now;
            dateEnd.Value = DateTime.Now;
            doneCheckbox.Checked = false;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        // добавление элемента в listTasks
        private void Add(Task task)
        {
            ListViewItem lvi = new ListViewItem(task.TaskName);
            lvi.Tag = task;
            listTasks.Items.Add(lvi);
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            Task task = new Task(textName.Text, textDesc.Text, dateStart.Value, dateEnd.Value, doneCheckbox.Checked);
            if (task.TaskName == "" || task.TaskDescription == "")
            {
                MessageBox.Show("Поля не могут быть пустыми");
            }
            else
            {
                Add(task);
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = task.TaskName;
                dataGridView1.Rows[n].Cells[1].Value = task.TaskDescription;
                dataGridView1.Rows[n].Cells[2].Value = task.DateStart;
                dataGridView1.Rows[n].Cells[3].Value = task.DateEnd;
                dataGridView1.Rows[n].Cells[4].Value = task.Done;
                ClearInput();
            }
            
        }
        private void SerializeXML(Tasks tasks)
        {
            XmlSerializer xml = new XmlSerializer(typeof(Tasks));
            using (FileStream fs = new FileStream(@"D:\Desktop\мирэа\технологии программирования\курсовая\TaskManagerV2\tasks.xml", FileMode.OpenOrCreate))
            {
                xml.Serialize(fs, tasks);
            }
        }

        private Tasks DeserializeXML()
        {   
            XmlSerializer xml = new XmlSerializer(typeof(Tasks));
            {
                using (FileStream fs = new FileStream(@"D:\Desktop\мирэа\технологии программирования\курсовая\TaskManagerV2\tasks.xml", FileMode.OpenOrCreate))
                {
                    Tasks tasks = (Tasks)xml.Deserialize(fs);
                    return tasks;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Tasks tasks = new Tasks();
            foreach(ListViewItem item in listTasks.Items)
            {
                if(item.Tag != null)
                {
                    tasks.TaskList.Add((Task)item.Tag);
                }
            }
            SerializeXML(tasks);
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            listTasks.Clear();
            dataGridView1.Rows.Clear();
            ClearInput();
            Tasks tasks = DeserializeXML();

            foreach (Task task in tasks.TaskList)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = task.TaskName;
                dataGridView1.Rows[n].Cells[1].Value = task.TaskDescription;
                dataGridView1.Rows[n].Cells[2].Value = task.DateStart;
                dataGridView1.Rows[n].Cells[3].Value = task.DateEnd;
                dataGridView1.Rows[n].Cells[4].Value = task.Done;
                Add(task);
            }
        }

        private void listTasks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listTasks.SelectedItems.Count == 1)
            {
                Task task = (Task)listTasks.SelectedItems[0].Tag;
                if (task != null)
                {
                    textName.Text = task.TaskName;
                    textDesc.Text = task.TaskDescription;
                    dateStart.Value = task.DateStart;
                    dateEnd.Value = task.DateEnd;
                    doneCheckbox.Checked = task.Done;
                }
            }
            else if (listTasks.SelectedItems.Count == 0)
            {
                ClearInput();
            }
        }

        private void changeBtn_Click(object sender, EventArgs e)
        {
            textName.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            textName.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textDesc.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            dateStart.Value = (DateTime)dataGridView1.SelectedRows[0].Cells[2].Value;
            dateEnd.Value = (DateTime)dataGridView1.SelectedRows[0].Cells[3].Value;
            doneCheckbox.Checked = (bool)dataGridView1.SelectedRows[0].Cells[4].Value;
        }
    }
}
