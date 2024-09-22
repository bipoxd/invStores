namespace invStore
{
    partial class FormVenta
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_finalizarVenta = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.txtCodigoProd = new System.Windows.Forms.TextBox();
            this.dgvCarrito = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbxMedioPago = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCarrito)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_finalizarVenta
            // 
            this.btn_finalizarVenta.Location = new System.Drawing.Point(644, 402);
            this.btn_finalizarVenta.Name = "btn_finalizarVenta";
            this.btn_finalizarVenta.Size = new System.Drawing.Size(128, 36);
            this.btn_finalizarVenta.TabIndex = 0;
            this.btn_finalizarVenta.Text = "Finalizar Venta";
            this.btn_finalizarVenta.UseVisualStyleBackColor = true;
            this.btn_finalizarVenta.Click += new System.EventHandler(this.btn_finalizarVenta_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(15, 402);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(128, 36);
            this.button2.TabIndex = 1;
            this.button2.Text = "Cancelar Venta";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // txtCodigoProd
            // 
            this.txtCodigoProd.Location = new System.Drawing.Point(12, 61);
            this.txtCodigoProd.Name = "txtCodigoProd";
            this.txtCodigoProd.Size = new System.Drawing.Size(215, 22);
            this.txtCodigoProd.TabIndex = 2;
            this.txtCodigoProd.TextChanged += new System.EventHandler(this.txtCodigoProd_TextChanged);
            // 
            // dgvCarrito
            // 
            this.dgvCarrito.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCarrito.Location = new System.Drawing.Point(15, 151);
            this.dgvCarrito.Name = "dgvCarrito";
            this.dgvCarrito.RowHeadersWidth = 51;
            this.dgvCarrito.RowTemplate.Height = 24;
            this.dgvCarrito.Size = new System.Drawing.Size(760, 218);
            this.dgvCarrito.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Agregar Producto";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(360, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "VENTA";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(360, 372);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(0, 16);
            this.lblTotal.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "Medio de Pago";
            // 
            // cbxMedioPago
            // 
            this.cbxMedioPago.FormattingEnabled = true;
            this.cbxMedioPago.Location = new System.Drawing.Point(15, 113);
            this.cbxMedioPago.Name = "cbxMedioPago";
            this.cbxMedioPago.Size = new System.Drawing.Size(212, 24);
            this.cbxMedioPago.TabIndex = 9;
            // 
            // FormVenta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(823, 471);
            this.Controls.Add(this.cbxMedioPago);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvCarrito);
            this.Controls.Add(this.txtCodigoProd);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btn_finalizarVenta);
            this.Name = "FormVenta";
            this.Text = "FormVenta";
            ((System.ComponentModel.ISupportInitialize)(this.dgvCarrito)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_finalizarVenta;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtCodigoProd;
        private System.Windows.Forms.DataGridView dgvCarrito;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbxMedioPago;
    }
}