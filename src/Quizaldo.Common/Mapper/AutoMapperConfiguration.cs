using AutoMapper;
using Quizaldo.Common.ServiceModels;
using Quizaldo.Common.ViewModels;
using Quizaldo.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quizaldo.Common.Mapper
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            this.CreateMap<Quiz, CreateQuizBindingModel>().ReverseMap();
            this.CreateMap<Quiz, QuizViewModel>().ReverseMap();
            this.CreateMap<Quiz, AllQuizzesViewModel>().ReverseMap();
            this.CreateMap<Quiz, SearchResultsViewModel>().ReverseMap();
            this.CreateMap<Question, QuestionViewModel>().ReverseMap();
            this.CreateMap<Question, AddQuestionBindingModel>().ReverseMap();
            this.CreateMap<UserResult, UserResultViewModel>().ReverseMap();
            this.CreateMap<QuizaldoUser, UsersRanklistViewModel>().ReverseMap();
            this.CreateMap<QuizServiceModel, QuizViewModel>().ReverseMap();
            this.CreateMap<AnswersServiceModel, AnswersBindingModel>().ReverseMap();
            this.CreateMap<QuestionSuggestion, SuggestQuestionBindingModel>().ReverseMap();
            this.CreateMap<QuestionSuggestion, AllQuestionSuggestionsViewModel>().ReverseMap();
            this.CreateMap<Question, QuestionSuggestion>().ReverseMap().ForMember(x => x.Id, opt => opt.Ignore());
        }
    }
}
