using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highscores {
    private struct Score {
        public string name;
        public int score;
    }

    private Score[] Scores;

    public Highscores() {
        Scores = new Score[10];
    }
}
