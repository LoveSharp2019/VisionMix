using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoga.Tools.OCR
{
    /// <summary>
    /// ocr文字识别自动模式下参数
    /// </summary>
    public enum OCRAutoParam
    {
        /// <summary>
        /// 字符最小对比度默认值 15
        /// </summary>
        min_contrast,
        /// <summary>
        /// 极性 默认值:both 值列表： 'dark_on_light'，'light_on_dark'， 'both'
        /// </summary>
        polarity,
        /// <summary>
        /// 像素中字符的最小高度。如果要分割任意高度的文本，则可以传递“auto”。
        /// “min_char_height”仅指字符。标点符号或分隔符的高度不受“min_char_height”的限制 。
        /// </summary>
        min_char_height,
        /// <summary>
        /// 像素中字符的最大高度。如果要分割任意高度的文本，则可以传递“auto”。
        /// “max_char_height”仅指字符。标点符号或分隔符的高度不受“max_char_height”的限制 
        /// </summary>
        max_char_height,
        /// <summary>
        /// 像素中字符的最小宽度。如果要分割任意宽度的文本，可以传递“auto”。
        /// “min_char_width”仅指字符。标点符号或分隔符的宽度不受'min_char_width'限制 。
        /// </summary>
        min_char_width,
        /// <summary>
        /// 像素中字符的最大宽度。如果要分割任意宽度的文本， 可以传递“auto”。
        /// “max_char_width”仅指字符。标点符号或分隔符的宽度不受'max_char_width'限制 。
        /// </summary>
        max_char_width,
        /// <summary>
        /// 像素中字符的最小行程宽度。如果要在文本分割过程中自动估计最小行程宽度，可以传递“自动”。
        /// “min_stroke_width”仅指字符。标点符号或分隔符的笔触宽度不受'min_stroke_width'限制。
        /// </summary>
        min_stroke_width,
        /// <summary>
        /// 像素中字符的最大行程宽度。如果自动在文本分割过程中估计最大行程宽度，则可以传递“自动”。
        /// “max_stroke_width”仅指字符。标点符号或分隔符的笔画宽度不受'max_stroke_width'限制。
        /// </summary>
        max_stroke_width,
        /// <summary>
        /// “true”=所选区域的边界地区应被丢弃，否则 =“false”。
        /// </summary>
        eliminate_border_blobs,
        /// <summary>
        /// 如果还应该返回标点符号（例如，点或逗号），则为 “true”。如果不返回标点符号，则为 “false”。
        /// </summary>
        return_punctuation,
        /// <summary>
        /// 如果还应该返回分隔符如减号或等号，则为 “true”。 如果没有分离器应该被返回，则为 'false'。
        /// </summary>
        return_separators,
        /// <summary>
        /// “true”，如果片段，诸如在“I”，应该被添加到分割的字符，否则“false”的点。请注意，这可能会导致噪音添加到分段字符。
        /// </summary>
        add_fragments,

        text_line_structure,
        text_line_separators,
        return_whole_line,
        mlp_classifier,       
    }
}
