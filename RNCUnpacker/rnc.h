#pragma once

#include <cstdint>

/*! Result status values from #rnc_inpack. */
enum class rnc_status
{
    ok, ///< Everything is fine
    file_is_not_rnc, ///< The file does not begin with an RNC signature
    huf_decode_error, ///< Error decoding the file
    file_size_mismatch, ///< The file size does not match the header
    packed_crc_error, ///< The compressed file does not match its checksum
    unpacked_crc_error ///< The uncompressed file does not match its checksum
};

rnc_status rnc_unpack(const uint8_t* input, uint8_t* output);