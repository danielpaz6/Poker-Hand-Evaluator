<img align="right" src="https://image.flaticon.com/icons/svg/2187/2187542.svg" width="260" />

# Texas holdem Rank Card Evaluator

[![forthebadge](https://forthebadge.com/images/badges/made-with-c-sharp.svg)](https://forthebadge.com) [![forthebadge](https://forthebadge.com/images/badges/built-with-love.svg)](https://forthebadge.com)

[![Build Status](https://travis-ci.org/joemccann/dillinger.svg?branch=master)](https://travis-ci.org/joemccann/dillinger)

Texas Holdem Rank Evaluator Algorithm made in C#. And also a Side Pots Calculator and its algorithm with in-depth explanation, examples and the math behind.

  - A system that gives a rank to a 7 cards hand.
  - Side pots calculator including money spreading
  - In-depth explenation with examples and pictures.

# Demonstration

<img align="center" src="/images/pokerdemo.gif" />

What we see in the example above is the unification of the cards that are on the table with the cards each player has.

Then, we run a pre-evaluator that will give us initial information about the same cards as the highest card, how many cards are in the sequence, how many have the same color, etc.

And then, in the last step, we will look into which Rank Group belongs to and then the cards in that group will be evaluated.


You can also:
  - Import and save files from GitHub, Dropbox, Google Drive and One Drive
  - Drag and drop markdown and HTML files into Dillinger
  - Export documents as Markdown, HTML and PDF

Markdown is a lightweight markup language based on the formatting conventions that people naturally use in email.  As [John Gruber] writes on the [Markdown site][df1]

> The overriding design goal for Markdown's
> formatting syntax is to make it as readable
> as possible. The idea is that a
> Markdown-formatted document should be
> publishable as-is, as plain text, without
> looking like it's been marked up with tags
> or formatting instructions.

This text you see here is *actually* written in Markdown! To get a feel for Markdown's syntax, type some text into the left window and watch the results in the right.

# In-depth explanation

## `EvaluateRankByHighestCards()` method

If we put aside different end cases for a moment, the main problem with solving the card evaluation problem is as follows:
We want a player with higher card to win players with less good cards,
and in the case of Taco we will move on to look at the next card and so on.

However, we don't want to make comparisons every time, so we want to work with a method of assessment and scoring for each player's hand instead.

### Simplifying the problem

Given a `K` **sorted** arrays with integers between 2 and 14, score it so values with high indexes will always be preferred on lower ones.
For example:

`[2,2,2,2,14]` will be preferred on `[12,12,12,12,13]` or `[13,13,13,13,13]` and in case of tie, we'll check the next number,

so for example: `[3,4,6,8,10]` wins `[4,4,5,8,10]` - if we take a look on the first and second numbers: 10 and 8, they both the same but when we take a look on the next number so 6 > 5 and that's why the 1st one will have better score.

### Reduction to base-13 number

Essentially we are describing a base-13 number. Each "digit" (array position) represents one of 13 distinct, ordered values.

We can calculate a ranking by converting the array to a 5 digit, base 13 value:

- Subtract 2 from each array element's value to normalize the element value to the range 0..12.
- Give each array element a score:

<img align="center" src="/images/base13.png" width="150" />

- We would actually want to power 13 by `Array.Length - Index` instead of `Index` because the most significant numbers placed on the higher indices.

**Method's Parameters:**

* `cards : List<string[]>` - the list of the cards, already sorted by their value.
* `excludeCardValue : int`, `excludeCardValue2 : int` - in some cases, we'll see that we don't need to check every single card, and in that way we can ignore some of them.
* `limitCheck : int` - in some cases, we would like to check only certain amount of cards.
* `normalize : double` - we would like the `score` of each hand to be `GroupScore` + `SubGroupScore` while SubGroupScore should be between [0,100], to make sure it will happen we need some sort of normalization variable.

## `GroupScore` and `SubGroupScore` evaluation

Some highlights that need to be made when evaluating the playing cards:

1. We need to give a rating to the group to which the cards belong, such as Straight or Two Pairs.
2. When the primary group is selected, it is necessary to evaluate how good the cards are within that group, in case some players will have the same primary group and we will need to evaluate within the same group of cards.
3. We will begin by examining the stronger groups first, and then only the weak ones because some of the weak ones are included in the weaks, such as Full House consists of Pair and Three of a Kind.

### Royal Flush `900`

The easier situation to be checked because the cards are constant, we need to check that the values are: 10, J, K, Q, A
and the suits of every one of those cards equal identical.

### Straight Flush `Group Range: [800, 900)`

We'll go through every suit and check if its count bigger than 4, if it does, we'll make another check if the numbers are in ascending order.

**Edge case:**

Ace can be straight of 2,3,4,5 or 10,J,Q,K for example:

| <img src="images/2-heart.png" width="60" /> | <img src="images/3-heart.png" width="60" /> | <img src="images/4-heart.png" width="60" /> | <img src="images/5-heart.png" width="60" /> | <img src="images/14-heart.png" width="60" /> |
| ------ | ------ | ------ | ------ | ------ |

### Four of a kind `Group Range: [700, 800)`

For the other cases ( including `Four of a kind` ) we'll sort descending the duplicates cards according to their amount.

**Edge case:**

The four of a kind is placed in the table's cards, in that case we need to check in each player for the 5th card, 
but we can't simply check for the highest card, since if the `Four of a kind` is the highest card, then we'll announce for a tie for no reason ( wrong calculation! )
Therefore, we'll use our method: `EvaluateRankByHighestCards()` to ignore the `Four of a kind`'s value and check for the highest card excluding it.

### Full House `Group Range: [600, 700)`


