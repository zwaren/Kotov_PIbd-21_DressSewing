﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DressSewingServiceDAL.BindingModels;
using DressSewingServiceDAL.Interfaces;
using DressSewingServiceDAL.ViewModels;

namespace DressSewingView
{
    public partial class FormCreateRequest : Form
    {

		public FormCreateRequest()
		{
			InitializeComponent();
		}

		private void FormCreateOrder_Load(object sender, EventArgs e)
		{
			try
			{
                List<DesignerViewModel> listC = APIClient.GetRequest<List<DesignerViewModel>>("api/Designer/GetList");
                if (listC != null)
				{
					comboBoxDesigner.DisplayMember = "DesignerFIO";
					comboBoxDesigner.ValueMember = "Id";
					comboBoxDesigner.DataSource = listC;
					comboBoxDesigner.SelectedItem = null;
				}
                List<DressViewModel> listP = APIClient.GetRequest<List<DressViewModel>>("api/Dress/GetList");
				if (listP != null)
				{
					comboBoxDress.DisplayMember = "DressName";
					comboBoxDress.ValueMember = "Id";
					comboBoxDress.DataSource = listP;
					comboBoxDress.SelectedItem = null;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
				MessageBoxIcon.Error);
			}
		}

		private void CalcSum()
		{
			if (comboBoxDress.SelectedValue != null &&
			!string.IsNullOrEmpty(textBoxCount.Text))
			{
				try
				{
					int id = Convert.ToInt32(comboBoxDress.SelectedValue);
					DressViewModel Dress = APIClient.GetRequest<DressViewModel>("api/Dress/Get/" + id); ;
					int count = Convert.ToInt32(textBoxCount.Text);
					textBoxSum.Text = (count * Dress.Price).ToString();
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
					MessageBoxIcon.Error);
				}
			}
		}

		private void textBoxCount_TextChanged(object sender, EventArgs e)
		{
			CalcSum();
		}

		private void comboBoxDress_SelectedIndexChanged(object sender, EventArgs e)
		{
			CalcSum();
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(textBoxCount.Text))
			{
				MessageBox.Show("Заполните поле Количество", "Ошибка",
				MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (comboBoxDesigner.SelectedValue == null)
			{
				MessageBox.Show("Выберите клиента", "Ошибка", MessageBoxButtons.OK,
				MessageBoxIcon.Error);
				return;
			}
			if (comboBoxDress.SelectedValue == null)
			{
				MessageBox.Show("Выберите изделие", "Ошибка", MessageBoxButtons.OK,
				MessageBoxIcon.Error);
				return;
			}
			try
			{
                APIClient.PostRequest<RequestBindingModel, bool>("api/Main/CreateRequest", new RequestBindingModel
				{
					DesignerId = Convert.ToInt32(comboBoxDesigner.SelectedValue),
					DressId = Convert.ToInt32(comboBoxDress.SelectedValue),
					Count = Convert.ToInt32(textBoxCount.Text),
					Sum = Convert.ToDecimal(textBoxSum.Text)
				});
				MessageBox.Show("Сохранение прошло успешно", "Сообщение",
				MessageBoxButtons.OK, MessageBoxIcon.Information);
				DialogResult = DialogResult.OK;
				Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
				MessageBoxIcon.Error);
			}
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}
        
    }
}
