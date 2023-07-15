import math


def generate_sphere(radius, lod):
    vertices = []
    indices = []
    index = 0

    for i in range(lod):
        theta1 = i * math.pi / lod
        theta2 = (i + 1) * math.pi / lod

        for j in range(lod):
            phi1 = j * 2 * math.pi / lod
            phi2 = (j + 1) * 2 * math.pi / lod

            # Generate vertices
            v1 = [
                radius * math.sin(theta1) * math.cos(phi1),
                radius * math.sin(theta1) * math.sin(phi1),
                radius * math.cos(theta1),
            ]
            v2 = [
                radius * math.sin(theta1) * math.cos(phi2),
                radius * math.sin(theta1) * math.sin(phi2),
                radius * math.cos(theta1),
            ]
            v3 = [
                radius * math.sin(theta2) * math.cos(phi2),
                radius * math.sin(theta2) * math.sin(phi2),
                radius * math.cos(theta2),
            ]
            v4 = [
                radius * math.sin(theta2) * math.cos(phi1),
                radius * math.sin(theta2) * math.sin(phi1),
                radius * math.cos(theta2),
            ]

            # Add vertices to list
            vertices.extend([v1, v2, v3, v4])

            # Generate indices
            indices.extend([index, index + 1, index + 2])
            indices.extend([index, index + 2, index + 3])
            index += 4

    return vertices, indices


def gen_file(vertices, indices, filename="output.model"):
    file = "//Auto generated\n#verts"

    for vert in vertices:
        file += f"\n{round(vert[0], 2)},"
        file += f"{round(vert[1], 2)},"
        file += f"{round(vert[2], 2)},"

    file += "\n#inds\n"

    for ind in indices:
        file += f"{ind},\n"

    with open(f"{filename}", "w") as f:
        f.write(file)


lod = input("Enter the lod (default:15): ")
if not lod:
    lod = "15"

lod = int(lod)
vertices, indices = generate_sphere(0.5, lod)

filename = input("Enter filename: ") + ".model"
gen_file(vertices, indices, filename)
