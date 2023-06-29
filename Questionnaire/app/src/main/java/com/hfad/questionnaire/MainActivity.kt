package com.hfad.questionnaire

import android.os.Bundle
import android.view.View
import android.widget.TextView
import androidx.appcompat.app.AppCompatActivity

class MainActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)
    }
    fun checkAnswer(v: View) {
        val clickedTextView = v as TextView
        val scoreView = findViewById<TextView>(R.id.score)

        var scoreCount = scoreView.text.toString().toInt()
        if(clickedTextView.tag == "Correct") {
            scoreCount += 2
            scoreView.text = scoreCount.toString()
        } else {
            scoreCount -= 1
            scoreView.text = scoreCount.toString()
        }
    }
}