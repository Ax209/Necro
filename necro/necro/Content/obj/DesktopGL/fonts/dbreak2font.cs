using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class DBreak2Font
{
    private readonly Texture2D _texture;
    private readonly int _glyphWidth;
    private readonly int _glyphHeight;
    private readonly int _columns;
    private readonly char _firstChar;
    private readonly char _lastChar;

    public DBreak2Font(Texture2D texture, int glyphWidth, int glyphHeight,
                         char firstChar = ' ', char lastChar = '~')
    {
        _texture = texture;
        _glyphWidth = glyphWidth;
        _glyphHeight = glyphHeight;
        _firstChar = firstChar;
        _lastChar = lastChar;

        _columns = texture.Width / glyphWidth;
    }

    public void DrawString(SpriteBatch spriteBatch, string text, Vector2 position, Color colour)
    {
        Vector2 cursor = position;

        foreach (char c in text)
        {
            if (c == '\n')
            {
                cursor.X = position.X;
                cursor.Y += _glyphHeight;
                continue;
            }

            if (c < _firstChar || c > _lastChar)
            {
                // Skip unsupported characters (or treat as space)
                cursor.X += _glyphWidth;
                continue;
            }

            int index = c - _firstChar;

            int x = (index % _columns) * _glyphWidth;
            int y = (index / _columns) * _glyphHeight;

            Rectangle src = new Rectangle(x, y, _glyphWidth, _glyphHeight);

            spriteBatch.Draw(_texture, cursor, src, colour);

            cursor.X += _glyphWidth;
        }
    }
}