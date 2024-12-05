using System;
using System.Collections.Generic;
using System.Text;

public class TonWalletAddressConverter
{
    private const byte NoBounceableTag = 0x51;
    private const byte TestOnlyTag = 0x80;
    private static readonly Dictionary<string, byte> ToByteMap = InitializeByteMap();

    /// <summary>
    /// Converts raw TON address to no-bounceable user-friendly format.
    /// </summary>
    /// <param name="hexAddress">Raw TON address formatted as "0:<hex string without 0x>".</param>
    /// <param name="testOnly">Convert address to test-only form.</param>
    /// <returns>User-friendly address.</returns>
    public static string ToUserFriendlyAddress(string hexAddress, bool testOnly = false)
    {
        var (wc, hex) = ParseHexAddress(hexAddress);

        byte tag = NoBounceableTag;
        if (testOnly)
        {
            tag |= TestOnlyTag;
        }

        var addr = new byte[34];
        addr[0] = tag;
        addr[1] = (byte)wc;
        Array.Copy(hex, 0, addr, 2, hex.Length);

        var addressWithChecksum = new byte[36];
        Array.Copy(addr, addressWithChecksum, addr.Length);
        Array.Copy(Crc16(addr), 0, addressWithChecksum, 34, 2);

        string addressBase64 = Convert.ToBase64String(addressWithChecksum);
        return addressBase64.Replace('+', '-').Replace('/', '_');
    }

    private static (int wc, byte[] hex) ParseHexAddress(string hexAddress)
    {
        if (!hexAddress.Contains(":"))
        {
            throw new ArgumentException($"Wrong address {hexAddress}. Address must include ':'.");
        }

        var parts = hexAddress.Split(':');
        if (parts.Length != 2)
        {
            throw new ArgumentException($"Wrong address {hexAddress}. Address must include ':' only once.");
        }

        if (!int.TryParse(parts[0], out int wc) || (wc != 0 && wc != -1))
        {
            throw new ArgumentException($"Wrong address {hexAddress}. WC must be 0 or -1, but {wc} received.");
        }

        string hex = parts[1];
        if (hex.Length != 64)
        {
            throw new ArgumentException($"Wrong address {hexAddress}. Hex part must be 64 bytes long, but {hex.Length} received.");
        }

        return (wc, HexToBytes(hex));
    }

    private static byte[] Crc16(byte[] data)
    {
        const int poly = 0x1021;
        int reg = 0;
        byte[] message = new byte[data.Length + 2];
        Array.Copy(data, message, data.Length);

        foreach (byte b in message)
        {
            int mask = 0x80;
            while (mask > 0)
            {
                reg <<= 1;
                if ((b & mask) != 0)
                {
                    reg += 1;
                }
                mask >>= 1;
                if (reg > 0xFFFF)
                {
                    reg &= 0xFFFF;
                    reg ^= poly;
                }
            }
        }
        return new byte[] { (byte)(reg >> 8), (byte)(reg & 0xFF) };
    }

    private static byte[] HexToBytes(string hex)
    {
        hex = hex.ToLower();
        if (hex.Length % 2 != 0)
        {
            throw new ArgumentException("Hex string must have a length that is a multiple of 2: " + hex);
        }

        byte[] result = new byte[hex.Length / 2];
        for (int i = 0; i < result.Length; i++)
        {
            string hexSubstring = hex.Substring(i * 2, 2);
            if (!ToByteMap.ContainsKey(hexSubstring))
            {
                throw new ArgumentException("Invalid hex character: " + hexSubstring);
            }
            result[i] = ToByteMap[hexSubstring];
        }
        return result;
    }

    private static Dictionary<string, byte> InitializeByteMap()
    {
        var map = new Dictionary<string, byte>();
        for (int i = 0; i <= 0xFF; i++)
        {
            string hex = i.ToString("x2");
            map[hex] = (byte)i;
        }
        return map;
    }
}
