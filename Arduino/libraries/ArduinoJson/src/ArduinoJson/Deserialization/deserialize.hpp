// ArduinoJson - https://arduinojson.org
// Copyright © 2014-2023, Benoit BLANCHON
// MIT License

#pragma once

#include <ArduinoJson/Deserialization/DeserializationError.hpp>
#include <ArduinoJson/Deserialization/DeserializationOptions.hpp>
#include <ArduinoJson/Deserialization/Reader.hpp>
#include <ArduinoJson/Polyfills/utility.hpp>
#include <ArduinoJson/StringStorage/StringStorage.hpp>

ARDUINOJSON_BEGIN_PRIVATE_NAMESPACE

template <template <typename, typename> class TDeserializer, typename TReader,
          typename TWriter>
TDeserializer<TReader, TWriter> makeDeserializer(MemoryPool* pool,
                                                 TReader reader,
                                                 TWriter writer) {
  ARDUINOJSON_ASSERT(pool != 0);
  return TDeserializer<TReader, TWriter>(pool, reader, writer);
}

template <template <typename, typename> class TDeserializer, typename TStream,
          typename... Args>
DeserializationError deserialize(JsonDocument& doc, TStream&& input,
                                 Args... args) {
  auto reader = makeReader(detail::forward<TStream>(input));
  auto data = VariantAttorney::getData(doc);
  auto pool = VariantAttorney::getPool(doc);
  auto options = makeDeserializationOptions(args...);
  doc.clear();
  return makeDeserializer<TDeserializer>(pool, reader,
                                         makeStringStorage(input, pool))
      .parse(*data, options.filter, options.nestingLimit);
}

template <template <typename, typename> class TDeserializer, typename TChar,
          typename Size, typename... Args,
          typename = typename enable_if<is_integral<Size>::value>::type>
DeserializationError deserialize(JsonDocument& doc, TChar* input,
                                 Size inputSize, Args... args) {
  auto reader = makeReader(input, size_t(inputSize));
  auto data = VariantAttorney::getData(doc);
  auto pool = VariantAttorney::getPool(doc);
  auto options = makeDeserializationOptions(args...);
  doc.clear();
  return makeDeserializer<TDeserializer>(pool, reader,
                                         makeStringStorage(input, pool))
      .parse(*data, options.filter, options.nestingLimit);
}

ARDUINOJSON_END_PRIVATE_NAMESPACE
