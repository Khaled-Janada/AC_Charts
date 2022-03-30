namespace AcCharts.Drawing;

public record Point(double X, double Y) {

    public static explicit operator Point((double x, double y) coordinates) {
        return new Point(coordinates.x, coordinates.y);
    }
}
