using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Array2D<T>
{
    private List<List<T>> _internalData;
    private List<List<T>> _transposedData;
    private int _rowCount;
    private int _colCount;

    public List<List<T>> DataAsLists
    {
        get { return _internalData; }
    }

    public List<List<T>> TransposedDataAsLists
    {
        get { return _transposedData; }
    }

    public T[,] DataAsArray
    {
        get
        {
            T[,] arrayData = new T[_colCount, _rowCount];
            for(int i = 0; i<_rowCount; i++)
            {
                List<T> currentLine = _internalData.ElementAt(i);
                for(int j=0; j<_colCount; j++)
                {
                    arrayData[j, i] = currentLine.ElementAt(j);
                }
            }
            return arrayData;
        }
    }

    public T[,] TransposeDataAsArray
    {
        get
        {
            T[,] arrayData = new T[_rowCount, _colCount];
            for (int i = 0; i < _rowCount; i++)
            {
                List<T> currentLine = _internalData.ElementAt(i);
                for (int j = 0; j < _colCount; j++)
                {
                    arrayData[i, j] = currentLine.ElementAt(j);
                }
            }
            return arrayData;
        }
    }

    public Array2D(T[,] array)
    {
        _internalData = new List<List<T>>();
        _rowCount = array.GetLength(1);
        _colCount = array.GetLength(0);
        for(int i=0; i<_rowCount; i++)
        {
            List<T> currentLine = new List<T>();
            for(int j=0; j<_colCount; j++)
            {
                currentLine.Add(array[j, i]);
            }
            _internalData.Add(currentLine);
        }
        UpdateTranspose();
    }

    private void UpdateTranspose()
    {
        _transposedData = new List<List<T>>();
        for(int j=0; j<_colCount; j++)
        {
            List<T> currentCol = new List<T>();
            currentCol.AddRange(_internalData.Select(r => r.ElementAt(j)));
            _transposedData.Add(currentCol);
        }
    }

    #region Elements manipulation
    public T GetElementAt(int x, int y)
    {
        if (x >= _colCount || y >= _rowCount || x < 0 || y < 0)
        {
            throw new ArgumentOutOfRangeException();
        }
        return _internalData.ElementAt(y).ElementAt(x);
    }

    public void InsertLineAt(List<T> line, int y)
    {
        if(line.Count != _colCount)
        {
            throw new ArgumentException("Inserted line must have the same number of elements as the rest of the matrix (" + _colCount + ")");
        }
        _internalData.Insert(y, line);
        _rowCount++;
    }

    public void InsertColAt(List<T> column, int x)
    {
        if (column.Count != _rowCount)
        {
            throw new ArgumentException("Inserted column must have the same number of elements as the rest of the matrix (" + _rowCount + ")");
        }
        for(int i=0; i<_rowCount; i++)
        {
            _internalData.ElementAt(i).Insert(x, column.ElementAt(i));
        }
        _colCount++;
    }

    public void UpdateElementAt(T newValue, int x, int y)
    {
        _internalData.ElementAt(y).RemoveAt(x);
        _internalData.ElementAt(y).Insert(x, newValue);
    }
    #endregion
}
