﻿using System.Collections.Generic;
using Vatsim.Vatis.Utils;
using Vatsim.Vatis.Weather.Objects;

namespace Vatsim.Vatis.Atis;

public class PrevailingVisibilityNode : AtisNode
{
    public PrevailingVisibilityNode()
    { }

    public override void Parse(Metar metar)
    {
        Parse(metar.PrevailingVisibility);
    }

    public void Parse(PrevailingVisibility node)
    {
        List<string> tts = new List<string>();
        if (node != null)
        {
            if (node.IsCavok)
            {
                tts.Add("CAV-OK");
            }
            else
            {
                if (node.VisibilityInMeters != null)
                {
                    if (node.VisibilityInMeters.VisibilityValue == 9999)
                    {
                        tts.Add("Visibility one, zero kilometers or more");
                    }
                    else
                    {
                        if (node.VisibilityInMeters.VisibilityValue > 5000)
                        {
                            tts.Add($"Visibility {node.VisibilityInMeters.VisibilityValue / 1000} {(Composite.UseVisibilitySuffix ? "kilometers" : "")}");
                        }
                        else
                        {
                            tts.Add($"Visibility {node.VisibilityInMeters.VisibilityValue.NumbersToWords()} {(Composite.UseVisibilitySuffix ? "meters" : "")}");
                        }
                    }
                }
                else
                {
                    if (node.RawValue.Contains("/"))
                    {
                        string result = "";
                        switch (node.RawValue)
                        {
                            case "M1/4SM":
                                result = "less than one quarter.";
                                break;
                            case "1 1/8SM":
                                result = "one and one eighth.";
                                break;
                            case "1 1/4SM":
                                result = "one and one quarter.";
                                break;
                            case "1 3/8SM":
                                result = "one and three eighths.";
                                break;
                            case "1 1/2SM":
                                result = "one and one half.";
                                break;
                            case "1 5/8SM":
                                result = "one and five eighths.";
                                break;
                            case "1 3/4SM":
                                result = "one and three quarters.";
                                break;
                            case "1 7/8SM":
                                result = "one and seven eighths.";
                                break;
                            case "2 1/4SM":
                                result = "two and one quarter.";
                                break;
                            case "2 1/2SM":
                                result = "two and one half.";
                                break;
                            case "2 3/4SM":
                                result = "two and three quarters.";
                                break;
                            case "1/16SM":
                                result = "one sixteenth.";
                                break;
                            case "1/8SM":
                                result = "one eighth.";
                                break;
                            case "3/16SM":
                                result = "three sixteenths.";
                                break;
                            case "1/4SM":
                                result = "one quarter.";
                                break;
                            case "5/16SM":
                                result = "five sixteenths.";
                                break;
                            case "3/8SM":
                                result = "three eighths.";
                                break;
                            case "1/2SM":
                                result = "one half.";
                                break;
                            case "5/8SM":
                                result = "five eighths.";
                                break;
                            case "3/4SM":
                                result = "three quarters.";
                                break;
                            case "7/8SM":
                                result = "seven eighths.";
                                break;
                        }
                        tts.Add($"Visibility {result}");
                    }
                    else
                    {
                        tts.Add($"Visibility {node.VisibilityInStatuteMiles.WholeNumber}");
                    }
                }

                if (node.VisibilityInMeters != null)
                {
                    if (node.VisibilityInMeters.VisibilityDirection != Weather.Enums.VisibilityDirection.NotSet)
                    {
                        var minVis = node.VisibilityInMeters.VisibilityValue;

                        switch (node.VisibilityInMeters.VisibilityDirection)
                        {
                            case Weather.Enums.VisibilityDirection.North:
                                tts.Add($"To the north {minVis.NumbersToWordsGroup()}");
                                break;
                            case Weather.Enums.VisibilityDirection.NorthEast:
                                tts.Add($"To the north-east {minVis.NumbersToWordsGroup()}");
                                break;
                            case Weather.Enums.VisibilityDirection.East:
                                tts.Add($"To the east {minVis.NumbersToWordsGroup()}");
                                break;
                            case Weather.Enums.VisibilityDirection.SouthEast:
                                tts.Add($"To the south-east {minVis.NumbersToWordsGroup()}");
                                break;
                            case Weather.Enums.VisibilityDirection.South:
                                tts.Add($"To the south {minVis.NumbersToWordsGroup()}");
                                break;
                            case Weather.Enums.VisibilityDirection.SouthWest:
                                tts.Add($"To the south-west {minVis.NumbersToWordsGroup()}");
                                break;
                            case Weather.Enums.VisibilityDirection.West:
                                tts.Add($"To the west {minVis.NumbersToWordsGroup()}");
                                break;
                            case Weather.Enums.VisibilityDirection.NorthWest:
                                tts.Add($"To the north-west {minVis.NumbersToWordsGroup()}");
                                break;
                        }
                    }
                }
            }

            VoiceAtis = string.Join(", ", tts).Trim(',');
            TextAtis = node.RawValue;
        }
    }
}