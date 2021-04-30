using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Maui.Graphics.Native.Gtk {

    public class NativeFontFamily : IFontFamily, IComparable<IFontFamily>, IComparable {

        static Pango.Context systemContext;

        static NativeFontFamily () {
            systemContext = Gdk.PangoHelper.ContextGet ();
        }

        private readonly string _name;
        private IFontStyle[] _fontStyles;

        public NativeFontFamily (string name) {
            _name = name;
        }

        public string Name => _name;

        public IFontStyle[] GetFontStyles () {
            return _fontStyles ??= GetAvailableFontStyles ();
        }

        private IEnumerable<(Pango.FontFamily family, Pango.FontDescription)> GetAvailableFamilyFaces (Pango.FontFamily family) {

            if (family != null) {
                foreach (var face in family.Faces)
                    yield return (family, face.Describe ());
            }

            yield break;
        }

        private IFontStyle[] GetAvailableFontStyles () {
            var fontFamilies = systemContext.FontMap?.Families.ToArray ();

            var styles = new List<IFontStyle> ();

            if (fontFamilies != null) {

                foreach (var font in fontFamilies.SelectMany (f=>GetAvailableFamilyFaces(f))) {

                    var id = font.family.Name;
                    var name = font.family.Name;
                    var weight = FontUtils.GetFontWeight (name);
                    var styleType = FontUtils.GetStyleType (name);

                    var fullName = _name;

                    if (i > 0)
                        fullName = $"{_name} {name}";

                    styles.Add (new NativeFontStyle (this, id, name, fullName, weight, styleType));
                }
            }

            styles.Sort ();

            return styles.ToArray ();
        }

        public override bool Equals (object obj) {
            if (obj == null)
                return false;

            if (ReferenceEquals (this, obj))
                return true;

            if (obj.GetType () != typeof(NativeFontFamily))
                return false;

            var other = (NativeFontFamily) obj;

            return _name == other._name;
        }

        public override int GetHashCode () {
            return (_name != null ? _name.GetHashCode () : 0);
        }

        public override string ToString () {
            return Name;
        }

        public int CompareTo (IFontFamily other) {
            return string.Compare (_name, other.Name, StringComparison.Ordinal);
        }

        public int CompareTo (object obj) {
            if (obj is IFontFamily other)
                return CompareTo (other);

            return -1;
        }

    }

}