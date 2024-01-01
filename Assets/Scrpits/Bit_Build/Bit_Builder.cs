using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bit_Builder
{
    public enum Bit_Type
    {
        FLOAT,
        BOOL,
    }

    public class Bit_Builder
    {
        //위치에 들어갈 타입 명시
        private Bit_Type[] containTypeArray;
        
        //실제 결과물이 들어가는 배열
        private byte[] containByteArray;

        //각 변수가 들어갈 변수
        private int[] locationBitArray;

        //타입이 몇개 들어 있는지 반환
        public int Length => containTypeArray.Length;

        public Bit_Builder(Bit_Type[] wantTypes)
        {
            int bitLength = 0;
            containTypeArray = wantTypes;

            locationBitArray = new int[wantTypes.Length];
            
            //각 타입을 돌면서 비트 시작 위치를 잡아주는 반복문입니다.
            for (int i = 0; i < containTypeArray.Length; ++i)
            {
                //int를 쓰기 때문에 32비트보다 큰 값은 자릅니다.
                if (containTypeArray[i] > (Bit_Type)32)
                {
                    containTypeArray[i] = (Bit_Type)32;
                };
                
                Debug.Log($"[Bit_Builder]target index - {i} / starting bit location value - {bitLength}");
                //지금까지 더해진 길이를 현재 위치에 넣습니다.
                locationBitArray[i] = bitLength;

                //비트 총길이에 현재 타입의 길이를 더합니다.
                bitLength += TypeLength(containTypeArray[i]);

                //반복..
            };
            
            containByteArray = new byte[(int)Math.Ceiling(bitLength / 8.0d)];
            Debug.Log("[Bit_Builder]containByteArray length : " + containByteArray);
        }
        
        private static int TypeLength(Bit_Type wantType)
        {
            if(wantType == Bit_Type.FLOAT)
            {
                return 32;
            }
            else
            {
                return (int)wantType;
            };
        }
        
        private void SetBool(int targetIndex, bool value)
        {
            //bit location은 위치를 8로 나눈 후, 나머지 값은 위치를 나타내어 준다 
            int bitLocation = locationBitArray[targetIndex] % 8;
            Debug.Log($"[SetBool]bitLocation : {bitLocation}");
            // 시작 비트의 값을 8로 나눈후 소수점을 버린 것과 같다.
            int byteLocation = locationBitArray[targetIndex] >> 3;
            Debug.Log($"[SetBool]byteLocation : {byteLocation}");
            if (value)
            {
                /*만약 들어온 값이 참이면, 방금 만든 비트 마스크와 원래 데이터를 Or 연산합니다.그렇게 되면 해당 위치에 1이 들어가게 됩니다.*/
                Debug.Log($"[SetBool]containByteArray[byteLocation] : prev : {containByteArray[byteLocation]}");
                containByteArray[byteLocation] |= (byte)(0x80 >> bitLocation);
                Debug.Log($"[SetBool]containByteArray[byteLocation] : true : {containByteArray[byteLocation]}");
            }
            else
            {
                //반대로 거짓을 받는다면, 비트 마스크를 뒤집습니다.뒤집은 상태로 데이터와 And 연산을 하게 되면, 원하는 위치 빼고는 원래 데이터를 유지하여
                //해당 위치를 무조건 0으로 바꿀 수 있게 됩니다.
                Debug.Log($"[SetBool]containByteArray[byteLocation] : prev : {containByteArray[byteLocation]}");
                containByteArray[byteLocation] &= (byte)~(0x80 >> bitLocation);
                Debug.Log($"[SetBool]containByteArray[byteLocation] : false : {containByteArray[byteLocation]}");
            };
        }

        private bool GetBool(int targetIndex)
        {
            int bitLocation = locationBitArray[targetIndex] % 8;
            byte targetByte = containByteArray[locationBitArray[targetIndex] >> 3];
           //해당 위치에 1이 있을 때, True라고 반환을 해주고, 0이 있으면 False라고 리턴을 해줄 겁니다.
            return (targetByte & (0x80 >> bitLocation)) > 0;
        }

        private void SetByte(int targetIndex, byte[] wantByte)
        {
            //시작 bit의 위치
            int bitLocation = locationBitArray[targetIndex] % 8;
            Debug.Log($"[SetByte] bitLocation : {bitLocation}");
            //시작 byte의 위치
            int byteLocation = locationBitArray[targetIndex] >> 3;
            Debug.Log($"[SetByte] byteLocation : {byteLocation}");
            //끝나는 bit의 위치
            int bitEnd = bitLocation + TypeLength(containTypeArray[targetIndex]);
            Debug.Log($"[SetByte] bitEnd : {bitEnd}");
            //끝나는 byte의 위치
            int byteEnd = (locationBitArray[targetIndex] + TypeLength(containTypeArray[targetIndex])) >> 3;
            Debug.Log($"[SetByte] byteEnd : {byteEnd}");
            //원래 값에서 모두 채워진 byte의 수
            int wholeByte = TypeLength(containTypeArray[targetIndex]) >> 3;
            //원래 값에서 마지막 바이트에 있는 bit 수
            int leftBit = TypeLength(containTypeArray[targetIndex]) % 8;
            //원래 값에서 마지막 바이트에 있는 bit 수 + 시작 bit의 위치
            int lastBit = leftBit + bitLocation;
            
            //클래스에 있는 byte배열의 길이를 넘어버리면 배열을 넘어갈 위험이 발생할 여지가 있으므로
            //끝나는 byte가 byte배열보다 큰 경우에는 실행하지 않고 끊습니다.
            if (byteEnd > containByteArray.Length)
            {
                return;
            };
            
            //넣으려는 변수의 길이가 8 이하
            if (1 < TypeLength(containTypeArray[targetIndex]) && TypeLength(containTypeArray[targetIndex]) < 8)
            {
                //첫 번째 비트에 모두 넣을 수 있는 상황입니다.
                if (bitEnd < 9)
                {
                    int bitMask = (0xff >> bitLocation) ^ (0xff >> bitEnd);

                    containByteArray[byteLocation] &= (byte)~bitMask;
                    containByteArray[byteLocation] |= (byte)(bitMask & (wantByte[0] << (8 - bitEnd)));
                    return;
                }
                //첫 번째 비트에 모두 넣을 수 없는 상황입니다.
                else
                {
                    int bitMask = (0xff >> bitLocation);

                    containByteArray[byteLocation] &= (byte)~bitMask;
                    containByteArray[byteLocation] |= (byte)(bitMask & wantByte[0]);

                    bitMask = ~(0xff >> (bitEnd - 8));

                    containByteArray[byteLocation + 1] &= (byte)~bitMask;
                    containByteArray[byteLocation + 1] |= (byte)(bitMask & (wantByte[0] << (8 - TypeLength(containTypeArray[targetIndex]))));
                };
            }
        }
    }
}
