using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RYM_Api.controllers;
using RYM_Api.models;

namespace RYM_Api
{
    public partial class Form1 : Form
    {
        private CharactersController CharactersController { get; set; }
        private Characters characters;

        public Form1()
        {
            InitializeComponent();
            CharactersController = new CharactersController();
            characters = new Characters();

            // Llama directamente al método GetCharacters en el constructor para cargar los datos al inicio
            LoadCharacters();
        }

        private async void LoadCharacters()
        {
            try
            {
                // Llama al método GetAllCharacters de forma asincrónica
                characters = await CharactersController.GetAllCharacters();

                // Limpia las filas existentes en el DataGridView antes de agregar nuevas
                dgvCharacters.Rows.Clear();

                // Puedes realizar otras operaciones después de obtener los personajes, como mostrarlos en el DataGridView
                foreach (var character in characters?.results)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(dgvCharacters);

                    row.Cells[0].Value = character.name;
                    row.Cells[1].Value = character.gender;
                    row.Cells[2].Value = character.species;
                    row.Cells[3].Value = character.origin.name;

                    dgvCharacters.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Puedes agregar lógica adicional aquí según las interacciones con el DataGridView
            LoadCharacters(); // O llama a GetCharacters aquí según tus necesidades
        }
    }
}