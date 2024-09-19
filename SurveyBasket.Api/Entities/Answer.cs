﻿namespace SurveyBasket.Api.Entities
{
    public sealed class Answer
    {
        public int Id { get; set; } 
        public string Content { get; set; } = string.Empty;
        public int QuestionId {  get; set; }
        public bool IsActive {  get; set; }


        public Question Question { get; set; } = default!;



    }
}
