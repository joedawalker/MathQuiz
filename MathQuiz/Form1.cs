using MathQuiz.Properties;
using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace MathQuiz
{
	public partial class MathQuiz : Form
	{
		private Random _randomizer = new Random();
		private int _addend1;
		private int _addend2;
		private int _minuend;
		private int _subtrahend;
		private int _multiplicand;
		private int _multiplier;
		private int _dividend;
		private int _divisor;
		private int _timeLeft;
		private const int QUIZ_LENGTH = 30;


		public MathQuiz()
		{
			InitializeComponent();
		}

		private void MathQuiz_Load( object sender, EventArgs e )
		{

		}

		/// <summary>
		/// Starts the timed math quiz.
		/// </summary>
		public void StartTheQuiz()
		{
			_addend1 = _randomizer.Next( 51 );
			_addend2 = _randomizer.Next( 51 );
			plusLeftLabel.Text = _addend1.ToString();
			plusRightLabel.Text = _addend2.ToString();
			sum.Value = 0;

			_minuend = _randomizer.Next( 1, 101 );
			_subtrahend = _randomizer.Next( 1, _minuend );
			minusLeftLabel.Text = _minuend.ToString();
			minusRightLabel.Text = _subtrahend.ToString();
			difference.Value = 0;

			_multiplicand = _randomizer.Next( 2, 11 );
			_multiplier = _randomizer.Next( 2, 11 );
			timesLeftLabel.Text = _multiplicand.ToString();
			timesRightLabel.Text = _multiplier.ToString();
			product.Value = 0;

			_divisor = _randomizer.Next( 2, 11 );
			int temporaryQuotient = _randomizer.Next( 2, 11 );
			_dividend = _divisor * temporaryQuotient;
			dividedLeftLabel.Text = _dividend.ToString();
			dividedRightLabel.Text = _divisor.ToString();
			quotient.Value = 0;

			_timeLeft = QUIZ_LENGTH;
			timeLabel.Text = string.Format( Resources.Time_Left_Seconds, _timeLeft );
			timer1.Start();
		}

		private void StartButton_Click( object sender, EventArgs e )
		{
			timeLabel.BackColor = DefaultBackColor;
			StartTheQuiz();
			startButton.Enabled = false;
		}

		private bool CheckTheAnswer()
		{
			return sum.Value == _addend1 + _addend2 &&
				   difference.Value == _minuend - _subtrahend &&
				   product.Value == _multiplicand * _multiplier &&
				   quotient.Value == _dividend / _divisor;
		}

		private void Timer1_Tick( object sender, EventArgs e )
		{
			if ( _timeLeft <= 5 )
			{
				timeLabel.BackColor = Color.Red;
			}

			if ( CheckTheAnswer() )
			{
				timer1.Stop();
				MessageBox.Show( Resources.Correct_Answers, Resources.Congradutulations_Exclamation );
				startButton.Enabled = true;
			}
			else if ( _timeLeft > 0 )
			{
				_timeLeft--;
				timeLabel.Text = string.Format( Resources.Time_Left_Seconds, _timeLeft );
			}
			else
			{
				timer1.Stop();
				timeLabel.Text = Resources.Time_Is_Up;
				MessageBox.Show( Resources.Time_Ran_Out_Message, Resources.Sorry_Exclamation );
				sum.Value = _addend1 + _addend2;
				difference.Value = _minuend - _subtrahend;
				product.Value = _multiplicand * _multiplier;
				quotient.Value = _dividend / _divisor;
				startButton.Enabled = true;
			}
		}

		private void Answer_Enter( object sender, EventArgs e )
		{
			if ( sender is NumericUpDown answerBox )
			{
				int lengthOfAnswer = answerBox.Value.ToString().Length;
				answerBox.Select( 0, lengthOfAnswer );
			}
		}

		private void SumCheckCorrect_ValueChanged( object sender, EventArgs e )
		{
			if ( sender is NumericUpDown answerBox && answerBox.Value == _addend1 + _addend2 )
			{
				PlayExclamation();
			}
		}

		private void DifferenceCheckCorrect_ValueChanged( object sender, EventArgs e )
		{
			if ( sender is NumericUpDown answerBox && answerBox.Value == _minuend - _subtrahend )
			{
				PlayExclamation();
			}
		}

		private void ProductCheckCorrect_ValueChanged( object sender, EventArgs e )
		{
			if ( sender is NumericUpDown answerBox && answerBox.Value == _multiplicand * _multiplier )
			{
				PlayExclamation();
			}
		}

		private void QuotientCheckCorrect_ValueChanged( object sender, EventArgs e )
		{
			NumericUpDown answerBox = sender as NumericUpDown;

			if ( answerBox != null && answerBox.Value == _dividend / _divisor )
			{
				PlayExclamation();
			}
		}

		private static void PlayExclamation()
		{
			SystemSounds.Exclamation.Play();
		}
	}
}
