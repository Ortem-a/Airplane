import matplotlib.pyplot as plt


def draw_coordinates_graphic():
    readFile = open('coordinates.txt', 'r')
    sepFile = readFile.read().split('\n')
    readFile.close()

    sepFile.pop();

    x = []
    y = []

    for plotPair in sepFile:
        plotPair = plotPair.replace(',', '.')
        xAndY = plotPair.split(';')
        x.append(float(xAndY[0]))
        y.append(float(xAndY[1]))

    # добавление графиков
    plt.plot(x, y, color='red', linewidth=2)

    # название всего графика и подписи к осям
    plt.title('Координаты')
    plt.xlabel('x')
    plt.ylabel('y')
    plt.grid()  # сетка по отметкам на осях

    plt.show()
    plt.savefig('coordinates.png', dpi=90)  # сохранение картинки


def draw_h_velocity_graphic():
    readFile = open('h_velocity.txt', 'r')
    sepFile = readFile.read().split('\n')
    readFile.close()

    sepFile.pop();

    x = []
    y = []

    for plotPair in sepFile:
        plotPair = plotPair.replace(',', '.')
        xAndY = plotPair.split(';')
        x.append(float(xAndY[0]))
        y.append(float(xAndY[1]))

    # добавление графиков
    plt.plot(x, y, color='red', linewidth=2)

    # название всего графика и подписи к осям
    plt.title('Горизонтальная скорость')
    plt.xlabel('Время, t')
    plt.ylabel('Скорость, м/с')
    plt.grid()  # сетка по отметкам на осях

    plt.show()
    plt.savefig('h_velocity.png', dpi=90)  # сохранение картинки


def draw_v_velocity_graphic():
    readFile = open('v_velocity.txt', 'r')
    sepFile = readFile.read().split('\n')
    readFile.close()

    sepFile.pop();

    x = []
    y = []

    for plotPair in sepFile:
        plotPair = plotPair.replace(',', '.')
        xAndY = plotPair.split(';')
        x.append(float(xAndY[0]))
        y.append(float(xAndY[1]))

    # добавление графиков
    plt.plot(x, y, color='red', linewidth=2)

    # название всего графика и подписи к осям
    plt.title('Вертикальная скорость')
    plt.xlabel('Время, t')
    plt.ylabel('Скорость, м/с')
    plt.grid()  # сетка по отметкам на осях

    plt.show()
    plt.savefig('v_velocity.png', dpi=90)  # сохранение картинки


draw_coordinates_graphic()
draw_h_velocity_graphic()
draw_v_velocity_graphic()
