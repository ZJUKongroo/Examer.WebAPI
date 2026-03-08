// Copyright (c) ZJUKongroo. All Rights Reserved.

namespace Examer.Helpers;

public class NotUniqueException : Exception { }

public class NotFoundException(string message) : Exception(message) { }

public class EmptyGuidException(string message) : Exception(message) { }
