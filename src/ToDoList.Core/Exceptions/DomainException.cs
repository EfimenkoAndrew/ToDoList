﻿namespace ToDoList.Core.Exceptions;

public class DomainException(string message) : Exception(message);