import math
import numpy as np

def generate_icosphere(radius, lod):
    # Define the icosahedron vertices
    t = (1.0 + math.sqrt(5.0)) / 2.0
    vertices = np.array([
        [-1, t, 0], [1, t, 0], [-1, -t, 0], [1, -t, 0],
        [0, -1, t], [0, 1, t], [0, -1, -t], [0, 1, -t],
        [t, 0, -1], [t, 0, 1], [-t, 0, -1], [-t, 0, 1]
    ])

    # Define the icosahedron faces
    faces = np.array([
        [0, 11, 5], [0, 5, 1], [0, 1, 7], [0, 7, 10], [0, 10, 11],
        [1, 5, 9], [5, 11, 4], [11, 10, 2], [10, 7, 6], [7, 1, 8],
        [3, 9, 4], [3, 4, 2], [3, 2, 6], [3, 6, 8], [3, 8, 9],
        [4, 9, 5], [2, 4, 11], [6, 2, 10], [8, 6, 7], [9, 8, 1]
    ])

    # Move the vertices towards the surface of a sphere
    for i in range(lod):
        # Normalize the vertices
        vertices = normalize(vertices)

        # Move the vertices towards the surface of a sphere
        vertices *= radius

        # Subdivide the faces recursively
        new_faces = []
        for face in faces:
            # Get the vertices of the face
            v1 = vertices[face[0]]
            v2 = vertices[face[1]]
            v3 = vertices[face[2]]

            # Calculate the midpoints of the edges
            v12 = normalize((v1 + v2) / 2)
            v23 = normalize((v2 + v3) / 2)
            v31 = normalize((v3 + v1) / 2)

            # Add the new vertices to the list
            vertices = np.vstack((vertices, v12, v23, v31))

            # Get the indices of the new vertices
            v12_index = len(vertices) - 3
            v23_index = len(vertices) - 2
            v31_index = len(vertices) - 1

            # Add the new faces
            new_faces.append([face[0], v12_index, v31_index])
            new_faces.append([face[1], v23_index, v12_index])
            new_faces.append([face[2], v31_index, v23_index])
            new_faces.append([v12_index, v23_index, v31_index])

        # Replace the old faces with the new ones
        faces = np.array(new_faces)

    # Normalize the vertices
    vertices = normalize(vertices)

    # Move the vertices towards the surface of a sphere
    vertices *= radius

    return vertices.tolist(), faces.tolist()

def normalize(v):
    norm = np.linalg.norm(v, axis=-1, keepdims=True)
    norm[norm == 0] = 1
    return v / norm


def gen_file(vertices, indices, filename="output.model"):
    file = "//Auto generated\n#verts"

    for vert in vertices:
        file += f"\n{round(vert[0], 6)},"
        file += f"{round(vert[1], 6)},"
        file += f"{round(vert[2], 6)},"

    file += "\n#inds\n"

    for ind in indices:
        file += f"{ind},\n"

    with open(f"{filename}", "w") as f:
        f.write(file)


lod = input("Enter the lod (default:3): ")
if not lod:
    lod = "3"

lod = int(lod)
vertices, indices = generate_icosphere(0.5, lod)

filename = input("Enter filename: ") + ".model"
gen_file(vertices, indices, filename)
