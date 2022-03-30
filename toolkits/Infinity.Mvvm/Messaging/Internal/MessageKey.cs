// ReSharper disable NotAccessedPositionalProperty.Global
namespace Infinity.Messaging.Internal;

internal readonly record struct MessageKey(Type MessageType, uint? Channel);
